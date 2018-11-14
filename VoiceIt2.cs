﻿using System;
using System.IO;
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
            client.AddDefaultHeader("platformId", "30");
        }

        public string GetPhrases(String contentLanguage)
        {
            var request = new RestRequest
            {
                Resource = "/phrases/" + contentLanguage,
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string GetAllUsers()
        {
            var request = new RestRequest
            {
                Resource = "/users",
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string CreateUser()
        {
            var request = new RestRequest
            {
                Resource = "/users",
                Method = RestSharp.Method.POST
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string CheckUserExists(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/users/" + userId,
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string DeleteUser(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/users/" + userId,
                Method = RestSharp.Method.DELETE
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string GetGroupsForUser(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/users/" + userId + "/groups",
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string GetAllGroups()
        {
            var request = new RestRequest
            {
                Resource = "/groups",
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string GetGroup(string groupId)
        {
            var request = new RestRequest
            {
                Resource = "/groups/" + groupId,
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string GroupExists(string groupId)
        {
            var request = new RestRequest
            {
                Resource = "/groups/" + groupId + "/exists",
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string CreateGroup(string description)
        {
            var request = new RestRequest
            {
                Resource = "/groups",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("description", description);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string AddUserToGroup(string groupId, string userId)
        {
            var request = new RestRequest
            {
                Resource = "/groups/addUser",
                Method = RestSharp.Method.PUT
            };
            request.AddParameter("groupId", groupId);
            request.AddParameter("userId", userId);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string RemoveUserFromGroup(string groupId, string userId)
        {
            var request = new RestRequest
            {
                Resource = "/groups/removeUser",
                Method = RestSharp.Method.PUT
            };
            request.AddParameter("groupId", groupId);
            request.AddParameter("userId", userId);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string DeleteGroup(string groupId)
        {
            var request = new RestRequest
            {
                Resource = "/groups/" + groupId,
                Method = RestSharp.Method.DELETE
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string GetAllVoiceEnrollments(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/voice/" + userId,
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string GetAllFaceEnrollments(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/face/" + userId,
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string GetAllVideoEnrollments(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/video/" + userId,
                Method = RestSharp.Method.GET
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string CreateVoiceEnrollment(string userId, string contentLanguage, string phrase, string recordingPath)
        {
            return CreateVoiceEnrollment(userId, contentLanguage, phrase, File.ReadAllBytes(recordingPath));
        }

        public string CreateVoiceEnrollment(string userId, string contentLanguage, string phrase, byte[] recording)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/voice",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("phrase", phrase);
            request.AddFileBytes("recording", recording, "recording");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string CreateVoiceEnrollmentByUrl(string userId, string contentLanguage, string phrase, string fileUrl)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/voice/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("phrase", phrase);
            request.AddParameter("fileUrl", fileUrl);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string CreateFaceEnrollment(string userId, string videoPath)
        {
            return CreateFaceEnrollment(userId, File.ReadAllBytes(videoPath));
        }

        public string CreateFaceEnrollment(string userId, byte[] video)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/face",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddFileBytes("video", video, "video");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string CreateFaceEnrollmentByUrl(string userId, string fileUrl)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/face/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("fileUrl", fileUrl);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string CreateVideoEnrollment(string userId, string contentLanguage, string phrase, string videoPath)
        {
            return CreateVideoEnrollment(userId, contentLanguage, phrase, File.ReadAllBytes(videoPath));
        }

        public string CreateVideoEnrollment(string userId, string contentLanguage, string phrase, byte[] video)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/video",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("phrase", phrase);
            request.AddFileBytes("video", video, "video");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string CreateVideoEnrollmentByUrl(string userId, string contentLanguage, string phrase, string fileUrl)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/video/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("phrase", phrase);
            request.AddParameter("fileUrl", fileUrl);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string DeleteVoiceEnrollment(string userId, int enrollmentId)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/voice/" + userId + "/" + enrollmentId.ToString(),
                Method = RestSharp.Method.DELETE
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string DeleteFaceEnrollment(string userId, int faceEnrollmentId)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/face/" + userId + "/" + faceEnrollmentId.ToString(),
                Method = RestSharp.Method.DELETE
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string DeleteVideoEnrollment(string userId, int enrollmentId)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/video/" + userId + "/" + enrollmentId.ToString(),
                Method = RestSharp.Method.DELETE
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string DeleteAllVoiceEnrollments(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/" + userId + "/voice",
                Method = RestSharp.Method.DELETE
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string DeleteAllFaceEnrollments(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/" + userId + "/face",
                Method = RestSharp.Method.DELETE
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string DeleteAllVideoEnrollments(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/" + userId + "/video",
                Method = RestSharp.Method.DELETE
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string DeleteAllEnrollments(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/enrollments/" + userId + "/all",
                Method = RestSharp.Method.DELETE
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string VoiceVerification(string userId, string contentLanguage, string phrase, string recordingPath)
        {
            return VoiceVerification(userId, contentLanguage, phrase, File.ReadAllBytes(recordingPath));
        }

        public string VoiceVerification(string userId, string contentLanguage, string phrase, byte[] recording)
        {
            var request = new RestRequest
            {
                Resource = "/verification/voice",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("phrase", phrase);
            request.AddFileBytes("recording", recording, "recording");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string VoiceVerificationByUrl(string userId, string contentLanguage, string phrase, string fileUrl)
        {
            var request = new RestRequest
            {
                Resource = "/verification/voice/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("phrase", phrase);
            request.AddParameter("fileUrl", fileUrl);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string FaceVerification(string userId, string videoPath)
        {
            return FaceVerification(userId, File.ReadAllBytes(videoPath));
        }

        public string FaceVerification(string userId, byte[] video)
        {
            var request = new RestRequest
            {
                Resource = "/verification/face",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddFileBytes("video", video, "video");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string FaceVerificationByUrl(string userId, string fileUrl)
        {
            var request = new RestRequest
            {
                Resource = "/verification/face/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("fileUrl", fileUrl);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string VideoVerification(string userId, string contentLanguage, string phrase, string videoPath)
        {
            return VideoVerification(userId, contentLanguage, phrase, File.ReadAllBytes(videoPath));
        }

        public string VideoVerification(string userId, string contentLanguage, string phrase, byte[] video)
        {
            var request = new RestRequest
            {
                Resource = "/verification/video",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("phrase", phrase);
            request.AddFileBytes("video", video, "video");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string VideoVerificationByUrl(string userId, string contentLanguage, string phrase, string fileUrl)
        {
            var request = new RestRequest
            {
                Resource = "/verification/video/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("userId", userId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("fileUrl", fileUrl);
            request.AddParameter("phrase", phrase);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string VoiceIdentification(string groupId, string contentLanguage, string phrase, string recordingPath)
        {
            return VoiceIdentification(groupId, contentLanguage, phrase, File.ReadAllBytes(recordingPath));
        }

        public string VoiceIdentification(string groupId, string contentLanguage, string phrase, byte[] recording)
        {
            var request = new RestRequest
            {
                Resource = "/identification/voice",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("groupId", groupId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("phrase", phrase);
            request.AddFileBytes("recording", recording, "recording");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string VoiceIdentificationByUrl(string groupId, string contentLanguage, string phrase, string fileUrl)
        {
            var request = new RestRequest
            {
                Resource = "/identification/voice/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("groupId", groupId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("phrase", phrase);
            request.AddParameter("fileUrl", fileUrl);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string FaceIdentification(string groupId, string videoPath) {
            return FaceIdentification(groupId, File.ReadAllBytes(videoPath));
        }

        public string FaceIdentification(string groupId, byte[] video)
        {
            var request = new RestRequest
            {
                Resource = "/identification/face",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("groupId", groupId);
            request.AddFileBytes("video", video, "video");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string FaceIdentificationByUrl(string groupId, string fileUrl)
        {
            var request = new RestRequest
            {
                Resource = "/identification/face/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("groupId", groupId);
            request.AddParameter("fileUrl", fileUrl);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string VideoIdentification(string groupId, string contentLanguage, string phrase, string videoPath)
        {
            return VideoIdentification(groupId, contentLanguage, phrase, File.ReadAllBytes(videoPath));
        }

        public string VideoIdentification(string groupId, string contentLanguage, string phrase, byte[] video)
        {
            var request = new RestRequest
            {
                Resource = "/identification/video",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("groupId", groupId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("phrase", phrase);
            request.AddFileBytes("video", video, "video");

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string VideoIdentificationByUrl(string groupId, string contentLanguage, string phrase, string fileUrl)
        {
            var request = new RestRequest
            {
                Resource = "/identification/video/byUrl",
                Method = RestSharp.Method.POST
            };
            request.AddParameter("groupId", groupId);
            request.AddParameter("contentLanguage", contentLanguage);
            request.AddParameter("fileUrl", fileUrl);
            request.AddParameter("phrase", phrase);

            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

        public string CreateUserToken(string userId)
        {
            var request = new RestRequest
            {
                Resource = "/users/" + userId + "/token",
                Method = RestSharp.Method.POST
            };
            IRestResponse response = client.Execute(request);
            return Task.FromResult(response.Content).GetAwaiter().GetResult();
        }

    }
}
