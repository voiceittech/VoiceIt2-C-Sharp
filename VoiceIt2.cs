using System;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace VoiceIt2API
{
    public class VoiceIt2
    {
        const string BASE_URL = "https://api.voiceit.io";
        RestClient client;

        public VoiceIt2(string apiKey, string apiToken)
        {
            client = new RestClient();
            client.BaseUrl = new Uri(BASE_URL);
            client.Authenticator = new HttpBasicAuthenticator(apiKey, apiToken);

        }

        public Task<string> GetAllUsers()
        {
            var request = new RestRequest
            {
                Resource = "/users",
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> CreateUser()
        {
            var request = new RestRequest
            {
                Resource = "/users",
                Method = RestSharp.Method.POST
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> GetUser(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/users/" + userId,
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> DeleteUser(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/users/" + userId,
                Method = RestSharp.Method.DELETE
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> GetGroupsForUser(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/users/" + userId + "/groups",
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> GetAllEnrollmentsForUser(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/" + userId,
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> DeleteAllEnrollmentsForUser(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/" + userId + "/all",
                Method = RestSharp.Method.DELETE
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> GetFaceEnrollmentsForUser(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/face/" + userId,
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> CreateVoiceEnrollment(string userId, string contentLanguage, Byte[] recording)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddFileBytes("recording", recording, "recording");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> CreateVoiceEnrollmentByUrl(string userId, string contentLanguage, string fileUrl)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("fileUrl", fileUrl);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> CreateFaceEnrollment(string userId, Byte[] video)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/face",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddFileBytes("video", video, "video");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> CreateFaceEnrollment(string userId, Byte[] video, bool doBlinkDetection)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/face",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("doBlinkDetection", doBlinkDetection);
            request.AddFileBytes("video", video, "video");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> CreateVideoEnrollment(string userId, string contentLanguage, Byte[] video)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/video",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddFileBytes("video", video, "video");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> CreateVideoEnrollment(string userId, string contentLanguage, Byte[] video, bool doBlinkDetection)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/video",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("doBlinkDetection", doBlinkDetection);
            request.AddFileBytes("video", video, "video");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> CreateVideoEnrollmentByUrl(string userId, string contentLanguage, string fileUrl)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/video/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("fileUrl", fileUrl);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> CreateVideoEnrollmentByUrl(string userId, string contentLanguage, string fileUrl, bool doBlinkDetection)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/video/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("doBlinkDetection", doBlinkDetection);
            request.AddParameter("fileUrl", fileUrl);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> DeleteFaceEnrollment(string userId, string faceEnrollmentId)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/face/" + userId + "/" + faceEnrollmentId,
                Method = RestSharp.Method.DELETE
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> GetAllGroups()
        {
            var request = new RestRequest
            {
                Resource = "/groups",
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> GetGroup(string groupId)
        {
            var request = new RestRequest
            {
                Resource = "/groups/" + groupId,
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> GroupExists(string groupId)
        {
            var request = new RestRequest
            {
                Resource = "/groups/" + groupId + "/exists",
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> CreateGroup(string description)
        {
            var request = new RestRequest
            {
                Resource = "/groups",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("description", description);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> AddUserToGroup(string groupId, string userId)
        {
            var request = new RestRequest
            {
                Resource = "/groups/addUser",
                Method = RestSharp.Method.PUT
            };
            request.AddParameter("groupId", groupId);
            request.AddParameter("userId", userId);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> RemoveUserFromGroup(string groupId, string userId)
        {
            var request = new RestRequest
            {
                Resource = "/groups/removeUser",
                Method = RestSharp.Method.PUT
            };
            request.AddParameter("groupId", groupId);
            request.AddParameter("userId", userId);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> DeleteGroup(string groupId)
        {
            var request = new RestRequest
            {
                Resource = "/groups/" + groupId,
                Method = RestSharp.Method.DELETE
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> VoiceVerification(string userId, string contentLanguage, Byte[] recording)
        {
            var request = new RestRequest
            {
                Resource = "/verification",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddFileBytes("recording", recording, "recording");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> VoiceVerificationByUrl(string userId, string contentLanguage, string fileUrl)
        {
            var request = new RestRequest
            {
                Resource = "/verification/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("fileUrl", fileUrl);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> FaceVerification(string userId, byte[] video)
        {
            var request = new RestRequest
            {
                Resource = "/verification/face",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddFileBytes("video", video, "video");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> FaceVerification(string userId, byte[] video, bool doBlinkDetection)
        {
            var request = new RestRequest
            {
                Resource = "/verification/face",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddFileBytes("video", video, "video");
            request.AddParameter("doBlinkDetection", doBlinkDetection);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> VideoVerification(string userId, string contentLanguage, byte[] video)
        {
            var request = new RestRequest
            {
                Resource = "/verification/video",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddFileBytes("video", video, "video");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> VideoVerification(string userId, string contentLanguage, byte[] video, bool doBlinkDetection)
        {
            var request = new RestRequest
            {
                Resource = "/verification/video",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("doBlinkDetection", doBlinkDetection);
            request.AddFileBytes("video", video, "video");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> VideoVerificationByUrl(string userId, string contentLanguage, string fileUrl)
        {
            var request = new RestRequest
            {
                Resource = "/verification/video/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("fileUrl", fileUrl);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> VideoVerificationByUrl(string userId, string contentLanguage, string fileUrl, bool doBlinkDetection)
        {
            var request = new RestRequest
            {
                Resource = "/verification/video/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("fileUrl", fileUrl);
            request.AddParameter("doBlinkDetection", doBlinkDetection);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> VoiceIdentification(string groupId, byte[] recording, string contentLanguage)
        {
            var request = new RestRequest
            {
                Resource = "/identification",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("groupId", groupId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddFileBytes("recording", recording, "recording");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> VoiceIdentificationByUrl(string groupId, string fileUrl, string contentLanguage)
        {
            var request = new RestRequest
            {
                Resource = "/identification/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("groupId", groupId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("fileUrl", fileUrl);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> VideoIdentification(string groupId, byte[] video, string contentLanguage)
        {
            var request = new RestRequest
            {
                Resource = "/identification/video",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("groupId", groupId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddFileBytes("video", video, "video");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> VideoIdentification(string groupId, byte[] video, string contentLanguage, bool doBlinkDetection)
        {
            var request = new RestRequest
            {
                Resource = "/identification/video",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("groupId", groupId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddFileBytes("video", video, "video");
            request.AddParameter("doBlinkDetection", doBlinkDetection);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> VideoIdentificationByUrl(string groupId, string fileUrl, string contentLanguage)
        {
            var request = new RestRequest
            {
                Resource = "/identification/video/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("groupId", groupId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("fileUrl", fileUrl);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }

        public Task<string> VideoIdentificationByUrl(string groupId, string fileUrl, string contentLanguage, bool doBlinkDetection)
        {
            var request = new RestRequest
            {
                Resource = "/identification/video/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("groupId", groupId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("fileUrl", fileUrl);
            request.AddParameter("doBlinkDetection", doBlinkDetection);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content);
        }
    }
}
