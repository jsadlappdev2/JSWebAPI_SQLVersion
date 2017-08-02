using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using Amazon.S3;
using Amazon.S3.Transfer;
using JSWebAPI_SQLVersion.Models;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;


namespace JSWebAPI_SQLVersion.Controllers
{
    public class filesinAWSS3Controller : ApiController
    {
        //API: upload file to AWS S3
        [HttpPost]
        [ActionName("UploadFileToAWSS3")]
        public string UploadFileToAWSS3([FromBody] filesinAWSS3 myfile, string youraccesskey,string yoursecuritykey)
        {


            string uploadfeedback = "";
            string file_saved_name = myfile.upload_person + "_" + DateTime.Now.ToString("yyyyMMddhhmmss")+"_" + myfile.file_orginal_name;
            //upload file
            try
            {
                string file_path = myfile.file_path + "\\" + myfile.file_orginal_name;
                FileStream fileToUpload = new FileStream(file_path, FileMode.Open, FileAccess.Read);
                uploadfeedback = sendMyFileToS3(fileToUpload, myfile.bucket_name, myfile.sub_folder, file_saved_name,youraccesskey,yoursecuritykey);
                if (uploadfeedback.Substring(0, 5) == "https") //success because of get the url for the file.
                {
                    MySqlConnection myConnection = new MySqlConnection();
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

                    MySqlCommand sqlCmd = new MySqlCommand();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "INSERT INTO file_on_awss3 (bucket_name,sub_folder,file_orginal_name,file_path,file_saved_name,flle_type,file_url,upload_person,upload_date) Values (@bucket_name,@sub_folder,@file_orginal_name,@file_path,@file_saved_name,@flle_type,@file_url,@upload_person,NOW())";
                    sqlCmd.Connection = myConnection;

                    sqlCmd.Parameters.AddWithValue("@bucket_name", myfile.bucket_name);
                    sqlCmd.Parameters.AddWithValue("@sub_folder", myfile.sub_folder);
                    sqlCmd.Parameters.AddWithValue("@file_orginal_name", myfile.file_orginal_name);
                    sqlCmd.Parameters.AddWithValue("@file_path", myfile.file_path);
                    sqlCmd.Parameters.AddWithValue("@file_saved_name", file_saved_name);
                    //get file type from file_original_name
                    string pos = myfile.file_orginal_name.Substring(myfile.file_orginal_name.IndexOf(".") + 1, myfile.file_orginal_name.Length - myfile.file_orginal_name.IndexOf(".")-1);
                    sqlCmd.Parameters.AddWithValue("@flle_type", pos);
                    sqlCmd.Parameters.AddWithValue("@file_url", uploadfeedback);
                    sqlCmd.Parameters.AddWithValue("@upload_person", myfile.upload_person);




                    try
                    {
                        myConnection.Open();
                        int rowInserted = sqlCmd.ExecuteNonQuery();
                        return uploadfeedback;
                    }
                    catch
                    {

                        //get password and send email success but insert history failed.
                        return uploadfeedback;

                    }
                    finally
                    {
                        myConnection.Close();
                    }

                }
                else
                {

                    return "Failed: " + uploadfeedback;
                }
            }
            catch (Exception e)
            {
                return "Failed: " + e.Message.ToString();

            }


        }


        //upload files to AWS S3 using stream
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
                string imageurl = "https://s3-ap-southeast-2.amazonaws.com/"+bucketName+"/"+subDirectoryInBucket+"/" + fileNameInS3;
                return imageurl;
            }
            catch (Exception err)
            {
                string imageurl = "Upload failed and Error is: " + err.Message.ToString();
                return imageurl;

            }

        }

        //upload files to AWSS3 using filepath
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
            catch (Exception)
            {
                //Label2.Text = "Error: " + err.Message.ToString();
                return false; //indicate that the file was sent

            }

        }
    }



}
