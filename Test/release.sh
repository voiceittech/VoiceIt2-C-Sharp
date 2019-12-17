#!/bin/bash
commit=$(git log -1 --pretty=%B | head -n 1)
version=$(echo $(nuget list voiceit | awk '{print $2}') | tr "." "\n")
set -- $version
major=$1
minor=$2
patch=$3
wrapperplatformversion=$(cat ~/platformVersion)
reponame=$(basename $(git remote get-url origin) | sed 's/.\{4\}$//')

if [[ $commit = *"RELEASE"* ]];
then

  if [[ $major = "" ]] || [[ $minor = "" ]] || [[ $patch = "" ]];
  then
    curl -X POST -H 'Content-type: application/json' --data '{
      "icon_url": "https://s3.amazonaws.com/voiceit-api2-testing-files/test-data/TravisCI-Mascot-1.png",
      "username": "Release Wrapper Gate",
        "attachments": [
            {
                "text": "Packaging '$reponame' version '$version' failed. because script could not get current version",
                "color": "danger"
            }
        ]
    }' 'https://hooks.slack.com/services/'$SLACKPARAM1'/'$SLACKPARAM2'/'$SLACKPARAM3
    echo "Unable to get current version: cannot release." 1>&2
    exit 1
  fi

  echo 'old version='$major'.'$minor'.'$patch
  if [[ $commit = *"RELEASEMAJOR"* ]];
  then
    releasetype="RELEASEMAJOR"
    major=$(($major+1))
    minor=0
    patch=0
  elif [[ $commit = *"RELEASEMINOR"* ]];
  then
    releasetype="RELEASEMINOR"
    minor=$(($minor+1))
    patch=0
  elif [[ $commit = *"RELEASEPATCH"* ]];
  then
    releasetype="RELEASEPATCH"
    patch=$(($patch+1))
  else
    curl -X POST -H 'Content-type: application/json' --data '{
      "icon_url": "https://s3.amazonaws.com/voiceit-api2-testing-files/test-data/TravisCI-Mascot-1.png",
      "username": "Release Wrapper Gate",
        "attachments": [
            {
                "text": "Packaging '$reponame' failed. You need to specify RELEASEMAJOR, RELEASEMINOR, or RELEASEPATCH in the commit title",
                "color": "danger"
            }
        ]
    }' 'https://hooks.slack.com/services/'$SLACKPARAM1'/'$SLACKPARAM2'/'$SLACKPARAM3
    echo "Must specify RELEASEMAJOR, RELEASEMINOR, or RELEASEPATCH in the title." 1>&2
    exit 1
  fi
  echo 'new version='$major'.'$minor'.'$patch
  version=$major'.'$minor'.'$patch

  if [[ $wrapperplatformversion = $version ]];
  then

    unzip VoiceIt2.zip
    cd Voiceit2

    echo '<Project Sdk="Microsoft.NET.Sdk">

      <PropertyGroup>
        <TargetFramework>netstandard2.0</TargetFramework>
        <PackageId>VoiceIt</PackageId>
        <Version>'$version'</Version>
        <Authors>Stephen Akers</Authors>
        <Company>VoiceIt</Company>
        <Title>VoiceIt</Title>
      </PropertyGroup>

      <ItemGroup>
        <PackageReference Include="RestSharp" Version="106.3.1" />
      </ItemGroup>

    </Project>' > VoiceIt.csproj

    cp /home/travis/build/voiceittech/VoiceIt2-C-Sharp/VoiceIt2.cs ./VoiceIt.cs

    nuget restore
    msbuild VoiceIt.csproj

    cd obj/Debug
    here=$(pwd)

    echo '<?xml version="1.0" encoding="utf-8"?>
    <package xmlns="http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd">
      <metadata>
        <id>VoiceIt</id>
        <version>'$version'</version>
        <title>VoiceIt</title>
        <authors>Stephen Akers</authors>
        <owners>Stephen Akers</owners>
        <requireLicenseAcceptance>false</requireLicenseAcceptance>
        <description>Package Description</description>
        <iconUrl>https://raw.githubusercontent.com/voiceittech/VoiceIt2-C-Sharp/master/voiceitlogo64.png</iconUrl>
        <dependencies>
          <group targetFramework=".NETStandard2.0">
            <dependency id="RestSharp" version="106.3.1" exclude="Build,Analyzers" />
          </group>
        </dependencies>
      </metadata>
      <files>
        <file src="'$here'/netstandard2.0/VoiceIt.dll" target="lib/netstandard2.0/VoiceIt.dll" />
      </files>
    </package>' > VoiceIt.$version.nuspec

    nuget pack VoiceIt.$version.nuspec
    dotnet nuget push VoiceIt.$version.nupkg -k $NUGETTOKEN -s https://api.nuget.org/v3/index.json 1>&2

    if [ "$?" != "0" ]
    then
      curl -X POST -H 'Content-type: application/json' --data '{
        "icon_url": "https://s3.amazonaws.com/voiceit-api2-testing-files/test-data/TravisCI-Mascot-1.png",
        "username": "Release Wrapper Gate",
          "attachments": [
              {
                  "text": "Packaging '$reponame' version '$version' failed.",
                  "color": "danger"
              }
          ]
      }' 'https://hooks.slack.com/services/'$SLACKPARAM1'/'$SLACKPARAM2'/'$SLACKPARAM3
      exit 1
    fi

    curl -X POST -H 'Content-type: application/json' --data '{
      "icon_url": "https://s3.amazonaws.com/voiceit-api2-testing-files/test-data/TravisCI-Mascot-1.png",
      "username": "Release Wrapper Gate",
        "attachments": [
            {
                "text": "Packaging '$reponame' version '$version' succeeded.",
                "color": "good"
            }
        ]
    }' 'https://hooks.slack.com/services/'$SLACKPARAM1'/'$SLACKPARAM2'/'$SLACKPARAM3


    #EMAIL
    # Just the git commit message title
    title=$(git log -1 --pretty=%B | head -n 1)
    git checkout master
    # Save the messages into an array called message
    IFS=$'\n' message=($(git log -1 --pretty=%B | sed -e '1,2d'))

    if [[ $title = *"SENDEMAIL"* ]];
    then
      formattedmessages=''
      for i in "${message[@]}"
      do
        formattedmessages=$formattedmessages'|'$i
      done

      json='{"authenticationPassword":"'$EMAILAUTHPASS'", "messages" : "'$formattedmessages'", "packageManaged": "true", "instructions": "nuget update</code></div><br /><p class=\"p1\"><span class=\"s1\">inside your project.</span></p><br />"}'
      curl -X POST -H "Content-Type: application/json" -d $json "https://api.voiceit.io/platform/30"
    fi

    exit 0

  else
    curl -X POST -H 'Content-type: application/json' --data '{
      "icon_url": "https://s3.amazonaws.com/voiceit-api2-testing-files/test-data/TravisCI-Mascot-1.png",
      "username": "Release Wrapper Gate",
        "attachments": [
            {
                "text": "Packaging '$reponame' version '$version' failed because the specified release version to update package management (specified by including '$releasetype' in the commit title) does not match the platform version inside the wrapper ('$wrapperplatformversion').",
                "color": "danger"
            }
        ]
    }' 'https://hooks.slack.com/services/'$SLACKPARAM1'/'$SLACKPARAM2'/'$SLACKPARAM3
    echo "Specified release version to update package management (specified by including "$releasetype" in the commit title) does not match the platform version in wrapper source." 1>&2
    exit 1
  fi
fi
