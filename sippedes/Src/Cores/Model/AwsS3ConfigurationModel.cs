namespace sippedes.Cores.Model;


public class AwsS3ConfigurationModel 
{
    public AwsS3ConfigurationModel()
    {
        BucketName = "";
        Region = "";
        AwsAccessKey = "";
        AwsSecretAccessKey = "";
        EndpointUrl = "";
    }
    public string EndpointUrl { get; set; }
    public string BucketName { get; set; }
    public string Region { get; set; }
    public string AwsAccessKey { get; set; }
    
    public string AwsSessionToken { get; set; }
    public string AwsSecretAccessKey { get; set; }
}