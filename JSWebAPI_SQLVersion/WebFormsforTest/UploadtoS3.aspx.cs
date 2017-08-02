using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Amazon.S3;
using Amazon.S3.Transfer;


namespace JSWebAPI_SQLVersion.WebFormsforTest
{
    public partial class UploadtoS3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

           //  string localFilePath = "C:\\Jerry Shen\\testfolder\\hill.JPG";
           //  FileStream fs = File.OpenRead(localFilePath);
           string     filePath= "C:\\Jerry Shen\\testfolder\\hill2.JPG";
           FileStream fileToUpload =    new FileStream(filePath, FileMode.Open, FileAccess.Read);

            string  fileurl = sendMyFileToS3(fileToUpload, "awss3dailylifehelperapp", "images", "hill2.JPG","youraccesskey", "yoursecuritykey");
            Label1.Text = fileurl;
        }

      

        public string sendMyFileToS3(System.IO.Stream localFilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3, string youraccesskey, string yoursecuritykey)
        {
            // input explained :
            // localFilePath = we will use a file stream , instead of path
            // bucketName : the name of the bucket in S3 ,the bucket should be already created
            // subDirectoryInBucket : if this string is not empty the file will be uploaded to
            // a subdirectory with this name
            // fileNameInS3 = the file name in the S3
            // create an instance of IAmazonS3 class ,in my case i choose RegionEndpoint.EUWest1
            // you can change that to APNortheast1 , APSoutheast1 , APSoutheast2 , CNNorth1
            // SAEast1 , USEast1 , USGovCloudWest1 , USWest1 , USWest2 . this choice will not
            // store your file in a different cloud storage but (i think) it differ in performance
            // depending on your location


            // IAmazonS3 client = new AmazonS3Client("Your Access Key", "Your Secrete Key", Amazon.RegionEndpoint.USWest2);
            IAmazonS3 client = new AmazonS3Client(youraccesskey, yoursecuritykey, Amazon.RegionEndpoint.APSoutheast2);

            // create a TransferUtility instance passing it the IAmazonS3 created in the first step
            TransferUtility utility = new TransferUtility(client);
            // making a TransferUtilityUploadRequest instance
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

            if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
            {
                request.BucketName = bucketName; //no subdirectory just bucket name
            }
            else
            {   // subdirectory and bucket name
                request.BucketName = bucketName + @"/" + subDirectoryInBucket;
            }
            request.Key = fileNameInS3; //file name up in S3
                                        //request.FilePath = localFilePath; //local file name
            request.InputStream = localFilePath;
            request.CannedACL = S3CannedACL.PublicReadWrite;
            try
            {
                utility.Upload(request); //commensing the transfer                                        
                //get url
                string imageurl = "https://s3-ap-southeast-2.amazonaws.com/awss3dailylifehelperapp/images/" + fileNameInS3;
                return imageurl;
            }
            catch(Exception err)
            {
                string imageurl= "Upload failed and Error is: " + err.Message.ToString();
                return imageurl; 

            }

        }


        public bool sendMyFileToS3byname(string localFilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3, string youraccesskey, string yoursecuritykey)
        {
            // input explained :
            // localFilePath = we will use a file stream , instead of path
            // bucketName : the name of the bucket in S3 ,the bucket should be already created
            // subDirectoryInBucket : if this string is not empty the file will be uploaded to
            // a subdirectory with this name
            // fileNameInS3 = the file name in the S3
            // create an instance of IAmazonS3 class ,in my case i choose RegionEndpoint.EUWest1
            // you can change that to APNortheast1 , APSoutheast1 , APSoutheast2 , CNNorth1
            // SAEast1 , USEast1 , USGovCloudWest1 , USWest1 , USWest2 . this choice will not
            // store your file in a different cloud storage but (i think) it differ in performance
            // depending on your location


            // IAmazonS3 client = new AmazonS3Client("Your Access Key", "Your Secrete Key", Amazon.RegionEndpoint.USWest2);
            IAmazonS3 client = new AmazonS3Client(youraccesskey, yoursecuritykey, Amazon.RegionEndpoint.APSoutheast2);

            // create a TransferUtility instance passing it the IAmazonS3 created in the first step
            TransferUtility utility = new TransferUtility(client);
            // making a TransferUtilityUploadRequest instance
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

            if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
            {
                request.BucketName = bucketName; //no subdirectory just bucket name
            }
            else
            {   // subdirectory and bucket name
                request.BucketName = bucketName + @"/" + subDirectoryInBucket;
            }
            request.FilePath = localFilePath;
            request.Key = fileNameInS3; //file name up in S3
                                        //request.FilePath = localFilePath; //local file name
          //  request.InputStream = localFilePath;
            request.CannedACL = S3CannedACL.PublicReadWrite;
            try
            {
                utility.Upload(request); //commensing the transfer
                return true; //indicate that the file was sent
            }
            catch (Exception err)
            {
                Label2.Text = "Error: " + err.Message.ToString();
                return false; //indicate that the file was sent

            }

        }
    }
}