using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_Signup.Classes
{
    static class FirestoreHelper
    {
        static string fireconfig = @"
        {
          ""type"": ""service_account"",
          ""project_id"": ""member-a1176"",
          ""private_key_id"": ""d58de613fd6fdec91f839bda8ed2e050fa53511d"",
          ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCpBGDkADqphb+q\ncUdTbzCcQUTIKxx2DPuwOaUNqHDRDxaauGYo3G8p0yTf+E4Hq6HOjzD+6Bj6Nih6\nRtnEgE7nkixrdqEHw2e6GTKOXB79T+pGdopSrbRDNH+lwGLq5xOt7C9pH3w3Hgii\n9zlFDt1KIWxq0/0JKvWSu6pW6+DgRBuAlxhw/ww97dkhsDWr9WLjOCcRQU5/g6z1\nVXaoCr7OGQqB+LWwUy9AWPt3ZFG0CAbKG4DR4hAIne7/+N4qJnBO6RhgiV3p8h/3\n7b2BOzcS78RjkMabeKdxAlOtUD0wpGkOw+qQJ1WGd6BzNnWzCOuesr0iCwZF9lA6\n+W7HW5HJAgMBAAECggEADnkdguiyHUoiu4mvvelLlFXSegGXElTSSKjnBWBjKMPN\n/7nCpcUaj85Q3gz5QZbk6DdCztE5MlPo62+dvkHuf834l8s0xuFoxHrb5WzmCnMJ\ncLc96Q6O3SJlu9/ZmJvpB3+uH6ZekKm1u57H/K3lLS/nhM5aYKqZFNOVJpRUUV/f\nuc1z2vRjOHRsi/JBNTApFd1aLaWJcjCeD3fZ1rv3ZH5aQfQ4MH/mv25vQ5BDajth\nzbqBXAC7PLPoH6nsmv0on/kNASSz8CSMajSiB4WX/6JYnIr6mP1VH5XeLly8PfTT\nM56UcFNEHTgJ1HIGmQYB9XU85jm2qKK/hAzgB8geAQKBgQDtscJCavxo3affrdsV\nGj7LJGPY4rCPNfRhz9a/p2IjCrBeVnLfItrqiprrkJ6G36S3bdOE8nQ6tRk3dSdY\nQkxqPZ+kHCJyBK75Um/Wn3FfOt0wg1wb2l8OY3i6AygKwrCZ3A0hRmR5P0QKMygP\nLffWHFCKTJPVc4/3piY8vEeIcQKBgQC2CJ6RZctAcOTbVCNaQQA0ku0ea+MUsade\nHnldHH5cm+qlOOmMnvt+8nMgx5esypbyQIhA6bv2pvHLRKh0tlKmxMHWVXnZPtS7\nxzNDM+pYD8zdRyKkXpJLR8pLjwTcuueIBBqXtRCSLLypkc7DcDzsHSbKH216y8RP\nJd4jx+yK2QKBgEA9OnMY1v7AaQ5avksFvNLKEvIa0fziaBnHQhKp+iveR6w3UPRd\nRyz8KMJhY3awGqQ4WmIj5KW5LAeA8hE4Wc6cuPhxYh3Ohjt7vB4VGV7TWdQyrEIa\n0nXhDE+5aqj91RzADAxiKeVa49id2sW/dqu3G01FRO77PH2BufPQLsUBAoGACC4Q\n+hWp8YZhMl3wjMC5AqPLlf9hH+/vxnH2IDIEl6LGA5CwJgqK4KSCeemeLjyYVeCC\nfvBB6w6LdQfrzfrXcfkLgNcsd4+0PY/xBMcTc8uZ+COXMe2yA0IBnC2cGVMozlro\n2wJe8UKjY7dSpEOp6S5SLOkGoWsAslHQTqlQPtkCgYAltqiQS7gkHUuU85v0LeA8\n5HSHvGPDijrTz8JcAKZemSXmKpbAFfIkU22ibwPWneaZRKTVNeOOI30ygagbH9nV\nQ/iEsGjXMluon1fq+no0R7+lfUPvmSpaKgSCiqBpYi5VY8J4nLj4M1+1ASKro+eM\ntYFPTStSfnabLPQgGOnMzg==\n-----END PRIVATE KEY-----\n"",
          ""client_email"": ""firebase-adminsdk-yh9mz@member-a1176.iam.gserviceaccount.com"",
          ""client_id"": ""117710122369178477805"",
          ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
          ""token_uri"": ""https://oauth2.googleapis.com/token"",
          ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
          ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-yh9mz%40member-a1176.iam.gserviceaccount.com"",
          ""universe_domain"": ""googleapis.com""
        } ";

        static string filepath = "";
        public static FirestoreDb Database  { get;private set; }

        public static void SetEnviromentVariable()
        {
            filepath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetRandomFileName())) + ".json";
            File.WriteAllText(filepath, fireconfig);
            File.SetAttributes(filepath, FileAttributes.Hidden);
            //------ Sao chep fireconfig va an no di.
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);// 1 biến process đọc thể làm việc.
            Database = FirestoreDb.Create("member-a1176");// da tao duoc database- có thể dùng để đọc - ghi data.
            //--
            File.Delete(filepath);
        }
    }
}
