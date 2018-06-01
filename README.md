# VoiceIt2-C-Sharp

A C# wrapper for VoiceIt's new API2.0 featuring Voice + Face Verification and Identification.

* [Getting Started](#getting-started)
* [Installation](#installation)
* [API Calls](#api-calls)
  * [Initialization](#initialization)
  * [User API Calls](#user-api-calls)
      * [Get All Users](#get-all-users)
      * [Create User](#create-user)
      * [Get User](#check-if-user-exists)
      * [Get Groups for User](#get-groups-for-user)
      * [Delete User](#delete-user)
  * [Group API Calls](#group-api-calls)
      * [Get All Groups](#get-all-groups)
      * [Create Group](#create-group)
      * [Get Group](#get-group)
      * [Delete Group](#delete-group)
      * [Group exists](#check-if-group-exists)
      * [Add User to Group](#add-user-to-group)
      * [Remove User from Group](#remove-user-from-group)      
  * [Enrollment API Calls](#enrollment-api-calls)
      * [Get All Enrollments for User](#get-all-enrollments-for-user)
      * [Delete Enrollment for User](#delete-enrollment-for-user)
      * [Create Audio Enrollment](#create-voice-enrollment)
      * [Create Video Enrollment](#create-video-enrollment)
  * [Verification API Calls](#verification-api-calls)
      * [Audio Verification](#voice-verification)
      * [Video Verification](#video-verification)
  * [Identification API Calls](#identification-api-calls)
      * [Audio Identification](#voice-identification)
      * [Video Identification](#video-identification)

## Getting Started

Sign up for a free Developer Account at <a href="https://voiceit.io/signup" target="_blank">VoiceIt.io</a> and activate API 2.0 from the settings page. Then you should be able view the API Key and Token. You can also review the HTTP Documentation at <a href="https://api.voiceit.io" target="_blank">api.voiceit.io</a>.

## Installation

Please make sure you have the RestSharp REST and HTTP Client package: https://www.nuget.org/packages/RestSharp/
```
dotnet add package RestSharp --version 106.3.0
```

The VoiceIt2 module can easily be fetched from nuget: https://www.nuget.org/packages/VoiceIt/1.0.1get.
```
dotnet add package VoiceIt --version 1.0.1
```

## API Calls

### Initialization

First initialize a reference with the API Credentials.

```cs
VoiceIt2 myVoiceIt = new VoiceIt2("API_KEY", "API_TOK");
```

### API calls

For each API call, you can use an awaiter to receive the result of the call. 
```cs
.GetAwaiter().GetResult()
```
Such as:
```cs
myVoiceIt.GetAllUsers().GetAwaiter().GetResult();
```

### User API Calls

#### Get All Users

Get all the users associated with the apiKey
```cs
myVoiceIt.GetAllUsers()
```

#### Create User

Create a new user
```cs
myVoiceIt.CreateUser()
```

#### Check if User Exists

Check whether a user exists for the given userId(begins with 'usr_')
```cs
myVoiceIt.GetUser("USER_ID_HERE").
```

#### Delete User

Delete user with given userId(begins with 'usr_')
```cs
myVoiceIt.DeleteUser("USER_ID_HERE")
```

#### Get Groups for User

Get a list of groups that the user with given userId(begins with 'usr_') is a part of
```cs
myVoiceIt.GetGroupsForUser("USER_ID_HERE")
```

### Group API Calls

#### Get All Groups

Get all the groups associated with the apiKey
```cs
myVoiceIt.GetAllGroups()
```

#### Get Group

Returns a group for the given groupId(begins with 'grp_')
```cs
myVoiceIt.GetGroup("GROUP_ID_HERE")
```

#### Check if Group Exists

Checks if group with given groupId(begins with 'grp_') exists
```cs
myVoiceIt.GroupExists("GROUP_ID_HERE")
```

#### Create Group

Create a new group with the given description
```cs
myVoiceIt.CreateGroup("Sample Group Description")
```

#### Add User to Group

Adds user with given userId(begins with 'usr_') to group with given groupId(begins with 'grp_')
```cs
myVoiceIt.AddUserToGroup("GROUP_ID_HERE", "USER_ID_HERE")
```

#### Remove User from Group

Removes user with given userId(begins with 'usr_') from group with given groupId(begins with 'grp_')

```cs
myVoiceIt.RemoveUserFromGroup( "GROUP_ID_HERE", "USER_ID_HERE")
```

#### Delete Group

Delete group with given groupId(begins with 'grp_'), note: tThis call does not delete any users, but simply deletes the group and disassociates the users from the group

```cs
myVoiceIt.DeleteGroup("GROUP_ID_HERE")
```

### Enrollment API Calls

#### Get All Enrollments for User

Gets all enrollment for user with given userId(begins with 'usr_')

```cs
myVoiceIt.GetAllEnrollmentsForuser("USER_ID_HERE")
```

#### Delete Enrollment for User

Delete enrollment for user with given userId(begins with 'usr_') and enrollmentId(integer)

```cs
myVoiceIt.DeleteEnrollmentForUser( "USER_ID_HERE", "ENROLLMENT_ID_HERE")
```

#### Create Voice Enrollment

Create audio enrollment for user with given userId(begins with 'usr_') and contentLanguage('en-US','es-ES' etc.). Note: File recording need to be no less than 1.2 seconds and no more than 5 seconds

```cs
myVoiceIt.CreateVoiceEnrollment("USER_ID_HERE", "CONTENT_LANGUAGE_HERE", Byte[] recording);
```

#### Create Video Enrollment

Create video enrollment for user with given userId(begins with 'usr_') and contentLanguage('en-US','es-ES' etc.). Note: File recording need to be no less than 1.2 seconds and no more than 5 seconds

```cs
myVoiceIt.CreateVideoEnrollment("USER_ID_HERE", "CONTENT_LANGUAGE_HERE", Byte[] video);
```

### Verification API Calls

#### Voice Verification

Verify user with the given userId(begins with 'usr_') and contentLanguage('en-US','es-ES' etc.). Note: File recording need to be no less than 1.2 seconds and no more than 5 seconds

```cs
myVoiceIt.VoiceVerification("USER_ID_HERE", "CONTENT_LANGUAGE_HERE", Byte[] recording)
```

#### Video Verification

Verify user with given userId(begins with 'usr_') and contentLanguage('en-US','es-ES' etc.). Note: File recording need to be no less than 1.2 seconds and no more than 5 seconds
```cs
myVoiceIt.VideoVerification("USER_ID_HERE", "CONTENT_LANGUAGE_HERE", Byte[] video)
```

### Identification API Calls

#### Voice Identification

Identify user inside group with the given groupId(begins with 'grp_') and contentLanguage('en-US','es-ES' etc.). Note: File recording need to be no less than 1.2 seconds and no more than 5 seconds

```cs
myVoiceIt.VoiceIdentification("GROUP_ID_HERE", "CONTENT_LANGUAGE_HERE", Byte[] recording)
```

#### Video Identification

Identify user inside group with the given groupId(begins with 'grp_') and contentLanguage('en-US','es-ES' etc.). Note: File recording need to be no less than 1.2 seconds and no more than 5 seconds

```cs
myVoiceIt.VideoIdentification("GROUP_ID_HERE", "CONTENT_LANGUAGE_HERE", Byte[] video)
```

## Author

Stephen Akers, stephen@voiceit.io

## License

VoiceIt2-C-Sharp is available under the MIT license. See the LICENSE file for more info.
