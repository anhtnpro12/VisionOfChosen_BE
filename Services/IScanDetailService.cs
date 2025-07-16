using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using VisionOfChosen_BE.DTOs.ScanDetail;
using VisionOfChosen_BE.Infra.Context;
using VisionOfChosen_BE.Infra.Models;

namespace VisionOfChosen_BE.Services
{
    public interface IScanDetailService
    {
        Task<string> CreateAsync(ScanDetailDto dto);
        Task<List<ScanDetailDto>> GetListAsync(ScanDetailQuery request);
        Task<ScanDetailDto?> GetByIdAsync(string id);
        Task<ScanDetailDto?> UpdateAsync(string id, ScanDetailDto dto);
        Task<bool> DeleteAsync(string id, string deletedBy);
        Task<ScanDashboardDto> GetScanDashboardAsync();

    }
    public class ScanDetailService : IScanDetailService
    {
        private readonly VisionOfChosen_Context _context;

        public ScanDetailService(VisionOfChosen_Context context)
        {
            _context = context;
        }

        // CREATE
        public async Task<string> CreateAsync(ScanDetailDto dto)
        {
            var scanDetail = new ScanDetail
            {
                FileName = dto.FileName,
                ScanDate = dto.ScanDate,
                Status = dto.Status,
                TotalResources = dto.TotalResources,
                DriftCount = dto.DriftCount,
                RiskLevel = dto.RiskLevel,
                Duration = dto.Duration,
                Drifts = dto.Drifts.Select(d => new Drift
                {
                    DriftCode = d.DriftCode,
                    ResourceType = d.ResourceType,
                    ResourceName = d.ResourceName,
                    RiskLevel = d.RiskLevel,
                    BeforeStateJson = d.BeforeStateJson != null ? JsonSerializer.Serialize(d.BeforeStateJson) : null,
                    AfterStateJson = d.AfterStateJson != null ? JsonSerializer.Serialize(d.AfterStateJson) : null,
                    AiExplanation = d.AiExplanation,
                    AiAction = d.AiAction
                }).ToList()
            }.Created(dto.CreatedBy!);

            _context.ScanDetails.Add(scanDetail);
            await _context.SaveChangesAsync();

            return scanDetail.id;
        }

        // READ - Get all with optional filters
        public async Task<List<ScanDetailDto>> GetListAsync(ScanDetailQuery request)
        {
            var query = _context.ScanDetails
                .Include(s => s.Drifts)
                .Where(s => !s.deleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.FileName))
                query = query.Where(s => s.FileName!.Contains(request.FileName));

            if (!string.IsNullOrEmpty(request.Status))
                query = query.Where(s => s.Status == request.Status);

            if (request.From.HasValue)
                query = query.Where(s => s.ScanDate >= request.From);

            if (request.To.HasValue)
                query = query.Where(s => s.ScanDate <= request.To);

            var filteredData = await query.ToListAsync();

            return filteredData.Select(s => new ScanDetailDto
            {
                Id = s.id,
                FileName = s.FileName,
                ScanDate = s.ScanDate,
                Status = s.Status,
                TotalResources = s.TotalResources,
                DriftCount = s.DriftCount,
                RiskLevel = s.RiskLevel,
                Duration = s.Duration,
                CreatedOn = s.created_on,
                CreatedBy = s.created_by,
                ModifiedBy = s.modified_by,
                Drifts = s.Drifts.Select(d => new DriftDto
                {
                    DriftCode = d.DriftCode,
                    ResourceType = d.ResourceType,
                    ResourceName = d.ResourceName,
                    RiskLevel = d.RiskLevel,
                    BeforeStateJson = string.IsNullOrEmpty(d.BeforeStateJson) ? null : JsonSerializer.Deserialize<object>(d.BeforeStateJson!),
                    AfterStateJson = string.IsNullOrEmpty(d.AfterStateJson) ? null : JsonSerializer.Deserialize<object>(d.AfterStateJson!),
                    AiExplanation = d.AiExplanation,
                    AiAction = d.AiAction
                }).ToList()
            }).ToList();
        }

        // READ - Get by Id
        public async Task<ScanDetailDto?> GetByIdAsync(string id)
        {
            var scan = await _context.ScanDetails
                .Include(s => s.Drifts)
                .FirstOrDefaultAsync(s => s.id == id && !s.deleted);

            if (scan == null) return null;

            return new ScanDetailDto
            {
                Id = scan.id,
                FileName = scan.FileName,
                ScanDate = scan.ScanDate,
                Status = scan.Status,
                TotalResources = scan.TotalResources,
                DriftCount = scan.DriftCount,
                RiskLevel = scan.RiskLevel,
                Duration = scan.Duration,
                CreatedOn = scan.created_on,
                CreatedBy = scan.created_by,
                Drifts = scan.Drifts.Select(d => new DriftDto
                {
                    DriftCode = d.DriftCode,
                    ResourceType = d.ResourceType,
                    ResourceName = d.ResourceName,
                    RiskLevel = d.RiskLevel,
                    BeforeStateJson = string.IsNullOrEmpty(d.BeforeStateJson) ? null : JsonSerializer.Deserialize<object>(d.BeforeStateJson!),
                    AfterStateJson = string.IsNullOrEmpty(d.AfterStateJson) ? null : JsonSerializer.Deserialize<object>(d.AfterStateJson!),
                    AiExplanation = d.AiExplanation,
                    AiAction = d.AiAction
                }).ToList()
            };
        }

        // UPDATE
        public async Task<ScanDetailDto?> UpdateAsync(string id, ScanDetailDto dto)
        {
            var existing = await _context.ScanDetails
                .Include(s => s.Drifts)
                .FirstOrDefaultAsync(s => s.id == id);

            if (existing == null) return null;

            existing.FileName = dto.FileName;
            existing.ScanDate = dto.ScanDate;
            existing.Status = dto.Status;
            existing.TotalResources = dto.TotalResources;
            existing.DriftCount = dto.DriftCount;
            existing.RiskLevel = dto.RiskLevel;
            existing.Duration = dto.Duration;
            existing.Modified(dto.ModifiedBy!);

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        // DELETE (soft delete)
        public async Task<bool> DeleteAsync(string id, string deletedBy)
        {
            var scan = await _context.ScanDetails.FindAsync(id);
            if (scan == null) return false;

            scan.Deleted(deletedBy);

            await _context.SaveChangesAsync();
            return true;
        }

        // OVERVIEW - Get scan dashboard summary
        public async Task<ScanDashboardDto> GetScanDashboardAsync()
        {
            var scans = await _context.ScanDetails
                .Include(s => s.Drifts)
                .Where(s => !s.deleted)
                .OrderByDescending(s => s.ScanDate)
                .ToListAsync();

            var latest = scans.FirstOrDefault();

            var result = new ScanDashboardDto
            {
                LatestScan = latest == null ? null : new LatestScanDto
                {
                    Id = latest.id,
                    FileName = latest.FileName,
                    ScanDate = latest.ScanDate,
                    DriftCount = latest.DriftCount,
                    RiskLevel = latest.RiskLevel,
                    Warnings = latest.Drifts.Count(d => d.RiskLevel == "medium"),
                    Status = latest.Status,
                    Duration = latest.Duration?.ToString("hh\\:mm\\:ss"),
                    ResourcesScanned = latest.TotalResources,
                    ChangesDetected = latest.Drifts.Count
                },
                ScanSummary = new ScanSummaryDto
                {
                    TotalScans = scans.Count,
                    SuccessfulScans = scans.Count(s => s.Status == "completed"),
                    FailedScans = scans.Count(s => s.Status == "failed"),
                    AvgDriftCount = scans.Count > 0 ? scans.Average(s => s.DriftCount) : 0,
                    TotalDriftsFound = scans.Sum(s => s.DriftCount),
                    CriticalIssues = scans.Sum(s => s.Drifts.Count(d => d.RiskLevel == "high"))
                },
                ScanHistory = scans.Select(s => new ScanHistoryItemDto
                {
                    Id = s.id,
                    FileName = s.FileName,
                    ScanDate = s.ScanDate,
                    Status = s.Status,
                    RiskLevel = s.RiskLevel,
                    DriftCount = s.DriftCount,
                    Warnings = s.Drifts.Count(d => d.RiskLevel == "medium"),
                    ResourcesScanned = s.TotalResources,
                    ChangesDetected = s.Drifts.Count,
                    Duration = s.Duration?.ToString("hh\\:mm\\:ss"),
                }).ToList()
            };

            return result;
        }

    }
}
