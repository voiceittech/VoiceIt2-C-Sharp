#!/bin/bash

#!/bin/bash
commit=$(cd /home/travis/build/voiceittech/VoiceIt2-C-Sharp && git log -1 --pretty=%B | head -n 1)
if [[ $commit = *"Release"* ]];
then
  unzip VoiceIt2.zip
  mv Voiceit2 ~/
  cd ~/Voiceit2

  version=$(nuget list voiceit | awk '{print $2}' | awk -F. -v OFS=. 'NF==1{print ++$NF}; NF>1{if(length($NF+1)>length($NF))$(NF-1)++; $NF=sprintf("%0*d", length($NF), ($NF+1)%(10^length($NF))); print}') 
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
  msbuild

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
          <dependency id="RestSharp" version="106.2.2" exclude="Build,Analyzers" />
        </group>
      </dependencies>
    </metadata>
    <files>
      <file src="'$here'/netstandard2.0/VoiceIt.dll" target="lib/netstandard2.0/VoiceIt.dll" />
    </files>
  </package>' > VoiceIt.$version.nuspec

  nuget pack VoiceIt.$version.nuspec
  dotnet nuget push VoiceIt.$version.nupkg -k $NUGETTOKEN -s https://api.nuget.org/v3/index.json
fi
