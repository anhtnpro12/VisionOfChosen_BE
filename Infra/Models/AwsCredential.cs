using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisionOfChosen_BE.Infra.Models
{
    [Table("aws_credential")]
    public class AwsCredential : AwsCredentialProperties
    {
    }

    public class AwsCredentialProperties : ExtendModel
    {
        [Column("aws_access_key_id")]
        public string? AwsAccessKeyId { get; set; }
        [Column("aws_secret_access_key")]
        public string? AwsSecretAccessKey { get; set; }
        [Column("aws_region")]
        public string? AwsRegion { get; set; }
        [Column("json_files")]
        public string? JsonPemFile { get; set; }

        [JsonIgnore]
        [NotMapped]
        public FileBase PemFile
        {
            get => JsonConvert.DeserializeObject<FileBase>(JsonPemFile ?? "{}") ?? new FileBase();
            set => JsonPemFile = JsonConvert.SerializeObject(value);
        }
    }
}
