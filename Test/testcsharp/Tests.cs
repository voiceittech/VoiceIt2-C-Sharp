using System;
using System.Runtime.CompilerServices;
using System.IO;
using System.Web.Script.Serialization;
using VoiceIt2API;

namespace testcsharpwrapper
{
    class MainClass
    {
        static void AssertEqual(string arg1, string arg2, [CallerLineNumber] int line = 0)
        {
            if (arg1 != arg2)
            {
                Console.WriteLine(arg1 + " does not == " + arg2 + " on line " + line);
                Environment.Exit(1);
            }
        }

        static void AssertEqual(int arg1, int arg2, [CallerLineNumber] int line = 0)
        {
            if (arg1 != arg2)
            {
                Console.WriteLine(arg1.ToString() + " does not == " + arg2.ToString() + " on line " + line);
                Environment.Exit(1);
            }
        }

        static void AssertGreaterThan(int arg1, int arg2, [CallerLineNumber] int line = 0)
        {
            if (arg1 <= arg2)
            {
                Console.WriteLine(arg1.ToString() + " is not > " + arg2.ToString() + " on line " + line);
                Environment.Exit(1);
            }
        }

        static void AssertNotEqual(string arg1, string arg2, [CallerLineNumber] int line = 0)
        {
            if (arg1 != arg2)
            {
                Console.WriteLine(arg1 + " is not != " + arg2 + " on line " + line);
                Environment.Exit(1);
            }
        }

        static void AssertNotEqual(int arg1, int arg2, [CallerLineNumber] int line = 0)
        {
            if (arg1 != arg2)
            {
                Console.WriteLine(arg1.ToString() + " is not != " + arg2.ToString() + " on line " + line);
                Environment.Exit(1);
            }
        }

        static Tuple<int, string> Deserialize(string JSONString)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic dobj = jsonSerializer.Deserialize<dynamic>(JSONString);
            int status = Int32.Parse(dobj["status"].ToString());
            string responseCode = dobj["responseCode"].ToString();
            return Tuple.Create(status, responseCode);
        }

        static string GetUserId(string JSONString)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic dobj = jsonSerializer.Deserialize<dynamic>(JSONString);
            return dobj["userId"].ToString();
        }

        static string GetAPIKey(string JSONString)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic dobj = jsonSerializer.Deserialize<dynamic>(JSONString);
            return dobj["apiKey"].ToString();
        }

        static string GetGroupId(string JSONString)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic dobj = jsonSerializer.Deserialize<dynamic>(JSONString);
            return dobj["groupId"].ToString();
        }

        static int GetEnrollmentId(string JSONString)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic dobj = jsonSerializer.Deserialize<dynamic>(JSONString);
            return Int32.Parse(dobj["id"].ToString());
        }

        static int GetFaceEnrollmentId(string JSONString)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic dobj = jsonSerializer.Deserialize<dynamic>(JSONString);
            return Int32.Parse(dobj["faceEnrollmentId"].ToString());
        }

        public static void Main(string[] args)
        {


            /**
             ****Test Basics****
            **/

            string viapikey = Environment.GetEnvironmentVariable("VIAPIKEY");
            string viapitoken = Environment.GetEnvironmentVariable("VIAPITOKEN");
            VoiceIt2 myVoiceIt = new VoiceIt2(viapikey, viapitoken);
            System.IO.File.WriteAllText(Environment.GetEnvironmentVariable("HOME") + "/platformVersion", myVoiceIt.GetVersion());
            string x = "";

            // Webhook Notifications
            myVoiceIt.AddNotificationUrl("https://voiceit.io");
            AssertEqual(myVoiceIt.GetNotificationUrl(), "https://voiceit.io");
            myVoiceIt.RemoveNotificationUrl();
            AssertEqual(myVoiceIt.GetNotificationUrl(), "");

            // Create User
            x = myVoiceIt.CreateUser();
            int status = 0;
            string responseCode = "";
            (status, responseCode) = Deserialize(x);
            string userId = GetUserId(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            // Get All Users
            x = myVoiceIt.GetAllUsers();
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            // Create Group
            x = myVoiceIt.CreateGroup("Sample Group Description");
            (status, responseCode) = Deserialize(x);
            string groupId = GetGroupId(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            // Get All Groups
            x = myVoiceIt.GetAllGroups();
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            // Add User to Group
            x = myVoiceIt.AddUserToGroup(groupId, userId);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            // Get all Groups for User
            x = myVoiceIt.GetGroupsForUser(userId);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            // Check if a Specific User Exists
            x = myVoiceIt.CheckUserExists(userId);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            // Get a Specific Group
            x = myVoiceIt.GetGroup(groupId);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            // Check if a Group Exists
            x = myVoiceIt.GroupExists(groupId);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            // Remove User From Group
            x = myVoiceIt.RemoveUserFromGroup(groupId, userId);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            // Create User Token
            x = myVoiceIt.CreateUserToken(userId, 5);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            // Expire User Token
            x = myVoiceIt.ExpireUserTokens(userId);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");


            // Delete Group
            x = myVoiceIt.DeleteGroup(groupId);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            // Delete User
            x = myVoiceIt.DeleteUser(userId);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            // All phrases
            x = myVoiceIt.GetPhrases("en-US");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            Console.WriteLine("**** Test Basics All Succeeded ****");
            
            /**
             ****Test Subaccounts****
            **/
            x = myVoiceIt.CreateUnmanagedSubAccount("Test", "Sharp", "", "", "");
            (status, responseCode) = Deserialize(x);
            string subAccountUnmanagedAPIKey = GetAPIKey(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateManagedSubAccount("Test", "Sharp", "", "", "");
            (status, responseCode) = Deserialize(x);
            string subAccountManagedAPIKey = GetAPIKey(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.SwitchSubAccountType(subAccountManagedAPIKey);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.RegenerateSubAccountAPIToken(subAccountManagedAPIKey);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.DeleteSubAccount(subAccountManagedAPIKey);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.DeleteSubAccount(subAccountUnmanagedAPIKey);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");
            

            Console.WriteLine("**** Test Subaccounts All Succeeded ****");
            /**
             ****Test Video****
            **/

            // Create Users
            x = myVoiceIt.CreateUser();
            string userId1 = GetUserId(x);

            x = myVoiceIt.CreateUser();
            string userId2 = GetUserId(x);

            // Create Video Enrollments
            x = myVoiceIt.CreateVideoEnrollment(userId1, "en-US", "never forget tomorrow is a new day", "/home/travis/videoEnrollmentB1.mov");
            (status, responseCode) = Deserialize(x);
            int enrollmentId1 = GetEnrollmentId(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVideoEnrollment(userId1, "en-US", "never forget tomorrow is a new day", "/home/travis/videoEnrollmentB2.mov");
            (status, responseCode) = Deserialize(x);
            int enrollmentId2 = GetEnrollmentId(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVideoEnrollment(userId1, "en-US", "never forget tomorrow is a new day", "/home/travis/videoEnrollmentB3.mov");
            (status, responseCode) = Deserialize(x);
            int enrollmentId3 = GetEnrollmentId(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVideoEnrollment(userId2, "en-US", "never forget tomorrow is a new day", File.ReadAllBytes("/home/travis/videoEnrollmentC1.mov"));
            (status, responseCode) = Deserialize(x);
            int enrollmentId4 = GetEnrollmentId(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVideoEnrollment(userId2, "en-US", "never forget tomorrow is a new day", File.ReadAllBytes("/home/travis/videoEnrollmentC2.mov"));
            (status, responseCode) = Deserialize(x);
            int enrollmentId5 = GetEnrollmentId(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVideoEnrollment(userId2, "en-US", "never forget tomorrow is a new day", File.ReadAllBytes("/home/travis/videoEnrollmentC3.mov"));
            (status, responseCode) = Deserialize(x);
            int enrollmentId6 = GetEnrollmentId(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            // Get All video Enrollments For User
            x = myVoiceIt.GetAllVideoEnrollments(userId1);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            // Video Verification
            x = myVoiceIt.VideoVerification(userId1, "en-US", "never forget tomorrow is a new day", "/home/travis/videoVerificationB1.mov");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            // Video Identification
            x = myVoiceIt.CreateGroup("Sample Group Description");
            groupId = GetGroupId(x);
            x = myVoiceIt.AddUserToGroup(groupId, userId1);
            x = myVoiceIt.AddUserToGroup(groupId, userId2);
            x = myVoiceIt.VideoIdentification(groupId, "en-US", "never forget tomorrow is a new day", "/home/travis/videoVerificationB1.mov");
            (status, responseCode) = Deserialize(x);
            userId = GetUserId(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");
            AssertEqual(userId, userId1);

            // Reset for ByURL
            x = myVoiceIt.DeleteGroup(groupId);
            x = myVoiceIt.DeleteUser(userId1);
            x = myVoiceIt.DeleteUser(userId2);
            x = myVoiceIt.CreateUser();
            userId1 = GetUserId(x);
            x = myVoiceIt.CreateUser();
            userId2 = GetUserId(x);
            x = myVoiceIt.CreateGroup("Sample Group Description");
            groupId = GetGroupId(x);
            myVoiceIt.AddUserToGroup(groupId, userId1);
            myVoiceIt.AddUserToGroup(groupId, userId2);

            // Create Video Enrollments by URL
            x = myVoiceIt.CreateVideoEnrollmentByUrl(userId1, "en-US", "never forget tomorrow is a new day", "https://drive.voiceit.io/files/videoEnrollmentB1.mov");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVideoEnrollmentByUrl(userId1, "en-US", "never forget tomorrow is a new day", "https://drive.voiceit.io/files/videoEnrollmentB2.mov");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVideoEnrollmentByUrl(userId1, "en-US", "never forget tomorrow is a new day", "https://drive.voiceit.io/files/videoEnrollmentB3.mov");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVideoEnrollmentByUrl(userId2, "en-US", "never forget tomorrow is a new day", "https://drive.voiceit.io/files/videoEnrollmentC1.mov");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVideoEnrollmentByUrl(userId2, "en-US", "never forget tomorrow is a new day", "https://drive.voiceit.io/files/videoEnrollmentC2.mov");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVideoEnrollmentByUrl(userId2, "en-US", "never forget tomorrow is a new day", "https://drive.voiceit.io/files/videoEnrollmentC3.mov");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            // Video Verification
            x = myVoiceIt.VideoVerificationByUrl(userId1, "en-US", "never forget tomorrow is a new day", "https://drive.voiceit.io/files/videoVerificationB1.mov");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            // Video Identification
            x = myVoiceIt.VideoIdentificationByUrl(groupId, "en-US", "never forget tomorrow is a new day", "https://drive.voiceit.io/files/videoVerificationB1.mov");
            (status, responseCode) = Deserialize(x);
            userId = GetUserId(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");
            AssertEqual(userId, userId1);

            Console.WriteLine("**** Test Video All Succeeded ****");
            myVoiceIt.DeleteAllEnrollments(userId1);
            myVoiceIt.DeleteAllEnrollments(userId2);
            myVoiceIt.DeleteUser(userId1);
            myVoiceIt.DeleteUser(userId2);
            myVoiceIt.DeleteGroup(groupId);

            /**
             ****Test Voice****
            **/

            x = myVoiceIt.CreateUser();
            userId1 = GetUserId(x);
            x = myVoiceIt.CreateUser();
            userId2 = GetUserId(x);
            x = myVoiceIt.CreateGroup("Sample Group Description");
            groupId = GetGroupId(x);
            myVoiceIt.AddUserToGroup(groupId, userId1);
            myVoiceIt.AddUserToGroup(groupId, userId2);

            // Voice Enrollments
            x = myVoiceIt.CreateVoiceEnrollment(userId1, "en-US", "never forget tomorrow is a new day", "/home/travis/enrollmentA1.wav");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVoiceEnrollment(userId1, "en-US", "never forget tomorrow is a new day", "/home/travis/enrollmentA2.wav");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVoiceEnrollment(userId1, "en-US", "never forget tomorrow is a new day", "/home/travis/enrollmentA3.wav");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVoiceEnrollment(userId2, "en-US", "never forget tomorrow is a new day", File.ReadAllBytes("/home/travis/enrollmentC1.wav"));
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVoiceEnrollment(userId2, "en-US", "never forget tomorrow is a new day", File.ReadAllBytes("/home/travis/enrollmentC2.wav"));
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVoiceEnrollment(userId2, "en-US", "never forget tomorrow is a new day", File.ReadAllBytes("/home/travis/enrollmentC3.wav"));
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            // Voice Verification
            x = myVoiceIt.VoiceVerification(userId1, "en-US", "never forget tomorrow is a new day", "/home/travis/verificationA1.wav");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            // Voice Identification
            x = myVoiceIt.VoiceIdentification(groupId, "en-US", "never forget tomorrow is a new day", "/home/travis/verificationA1.wav");
            (status, responseCode) = Deserialize(x);
            userId = GetUserId(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");
            AssertEqual(userId, userId1);

            // Reset for URL
            x = myVoiceIt.DeleteGroup(groupId);
            x = myVoiceIt.DeleteUser(userId1);
            x = myVoiceIt.DeleteUser(userId2);
            x = myVoiceIt.CreateUser();
            userId1 = GetUserId(x);
            x = myVoiceIt.CreateUser();
            userId2 = GetUserId(x);
            x = myVoiceIt.CreateGroup("Sample Group Description");
            groupId = GetGroupId(x);
            myVoiceIt.AddUserToGroup(groupId, userId1);
            myVoiceIt.AddUserToGroup(groupId, userId2);

            // Create Voice Enrollments by URL
            x = myVoiceIt.CreateVoiceEnrollmentByUrl(userId1, "en-US", "never forget tomorrow is a new day", "https://drive.voiceit.io/files/enrollmentA1.wav");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVoiceEnrollmentByUrl(userId1, "en-US", "never forget tomorrow is a new day", "https://drive.voiceit.io/files/enrollmentA2.wav");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVoiceEnrollmentByUrl(userId1, "en-US", "never forget tomorrow is a new day", "https://drive.voiceit.io/files/enrollmentA3.wav");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");


            x = myVoiceIt.CreateVoiceEnrollmentByUrl(userId2, "en-US", "never forget tomorrow is a new day", "https://drive.voiceit.io/files/enrollmentC1.wav");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVoiceEnrollmentByUrl(userId2, "en-US", "never forget tomorrow is a new day", "https://drive.voiceit.io/files/enrollmentC2.wav");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateVoiceEnrollmentByUrl(userId2, "en-US", "never forget tomorrow is a new day", "https://drive.voiceit.io/files/enrollmentC3.wav");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            // Voice Verification By URL
            x = myVoiceIt.VoiceVerificationByUrl(userId1, "en-US", "never forget tomorrow is a new day", "https://drive.voiceit.io/files/verificationA1.wav");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            // Voice Identification By URL
            x = myVoiceIt.VoiceIdentificationByUrl(groupId, "en-US", "never forget tomorrow is a new day", "https://drive.voiceit.io/files/verificationA1.wav");
            (status, responseCode) = Deserialize(x);
            userId = GetUserId(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");
            AssertEqual(userId, userId1);

            Console.WriteLine("**** Test Voice All Succeeded ****");
            myVoiceIt.DeleteAllEnrollments(userId1);
            myVoiceIt.DeleteAllEnrollments(userId2);
            myVoiceIt.DeleteUser(userId1);
            myVoiceIt.DeleteUser(userId2);
            myVoiceIt.DeleteGroup(groupId);

            /**
             ****Test Face****
            **/

            x = myVoiceIt.CreateUser();
            userId1 = GetUserId(x);
            x = myVoiceIt.CreateUser();
            userId2 = GetUserId(x);
            x = myVoiceIt.CreateGroup("Sample Group Description");
            groupId = GetGroupId(x);
            myVoiceIt.AddUserToGroup(groupId, userId1);
            myVoiceIt.AddUserToGroup(groupId, userId2);

            // Create Face Enrollments
            x = myVoiceIt.CreateFaceEnrollment(userId1, "/home/travis/faceEnrollmentB1.mp4");
            (status, responseCode) = Deserialize(x);
            int faceEnrollmentId1 = GetFaceEnrollmentId(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateFaceEnrollment(userId1, "/home/travis/faceEnrollmentB2.mp4");
            (status, responseCode) = Deserialize(x);
            int faceEnrollmentId2 = GetFaceEnrollmentId(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateFaceEnrollment(userId1, "/home/travis/faceEnrollmentB3.mp4");
            (status, responseCode) = Deserialize(x);
            int faceEnrollmentId3 = GetFaceEnrollmentId(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            // Create Face Enrollments
            x = myVoiceIt.CreateFaceEnrollment(userId2, "/home/travis/videoEnrollmentC1.mov");
            (status, responseCode) = Deserialize(x);
            int faceEnrollment2Id1 = GetFaceEnrollmentId(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateFaceEnrollment(userId2, "/home/travis/videoEnrollmentC2.mov");
            (status, responseCode) = Deserialize(x);
            int faceEnrollment2Id2 = GetFaceEnrollmentId(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            x = myVoiceIt.CreateFaceEnrollment(userId2, "/home/travis/videoEnrollmentC3.mov");
            (status, responseCode) = Deserialize(x);
            int faceEnrollment2Id3 = GetFaceEnrollmentId(x);
            AssertEqual(status, 201);
            AssertEqual(responseCode, "SUCC");

            // Face Verification
            x = myVoiceIt.FaceVerification(userId1, "/home/travis/faceVerificationB1.mp4");
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            // Face Identification
            x = myVoiceIt.FaceIdentification(groupId, "/home/travis/faceVerificationB1.mp4");
            (status, responseCode) = Deserialize(x);
            userId = GetUserId(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");
            AssertEqual(userId, userId1);

            // Face Identification By URL
            x = myVoiceIt.FaceIdentificationByUrl(groupId, "https://drive.voiceit.io/files/faceVerificationB1.mp4");
            (status, responseCode) = Deserialize(x);
            userId = GetUserId(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");
            AssertEqual(userId, userId1);

            x = myVoiceIt.DeleteAllEnrollments(userId2);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            Console.WriteLine("**** Test Face All Succeeded ****");

            /**
             ****Test Delete All ****
            **/

            x = myVoiceIt.DeleteAllEnrollments(userId);
            (status, responseCode) = Deserialize(x);
            AssertEqual(status, 200);
            AssertEqual(responseCode, "SUCC");

            Console.WriteLine("**** Test Delete All Succeeded ****");

            myVoiceIt.DeleteUser(userId1);
            myVoiceIt.DeleteUser(userId2);

        }
    }
}
