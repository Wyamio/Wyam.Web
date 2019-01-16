Title: Version 2.0
Published: 11/24/2018
---
<div style="width:100%;height:0;padding-bottom:68%;position:relative;"><iframe src="https://giphy.com/embed/rl0FOxdz7CcxO" width="100%" height="100%" style="position:absolute" frameBorder="0" class="giphy-embed" allowFullScreen></iframe></div><p><a href="https://giphy.com/gifs/excited-ron-paul-its-happening-rl0FOxdz7CcxO">via GIPHY</a></p>

I'm thrilled to announce the release of Wyam 2.0 for .NET Core. This has been a long time coming. The [corresponding issue](https://github.com/Wyamio/Wyam/issues/300) dates back to May 2016 and I've been working on it ever since (with the help of our awesome community). There's a few reasons why it's taken so long, the most important being that Wyam uses a whole bunch of dependencies to enable all the various modules, and each of those had to be updated to .NET Standard before we could port the primary Wyam application.

Please keep an eye out for problems and [report them](https://github.com/Wyamio/Wyam/issues) if you find any. A lot of work has gone into development and testing for this version, but changing runtimes is always a big shift and it's entirely possible (even likely) some things fell through the cracks. I'll be on the lookout for problems and will address them as quickly as I can.

# What Changed?

In reality, not a lot has changed other than the shift to .NET Core and the tooling to support that. I intentionally didn't change any of the underlying architecture for this initial release. Wyam is still shipped as a [ZIP archive](https://github.com/Wyamio/Wyam/releases) and an [old-school NuGet tools package](https://www.nuget.org/packages/Wyam/) (mainly for use from [Cake](https://cakebuild.net/)). I also added a [.NET Core global tool package](https://www.nuget.org/packages/Wyam.Tool/).

The big breaking change here is that Wyam now requires .NET Core 2.x to run. You must have the [.NET Core runtime](https://www.microsoft.com/net/download) installed on your system to use Wyam. A .NET Framework release is no longer shipped (though that could be reconsidered if there's a big outcry about .NET Framework support).

## Global Tool

The [global tool package](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) is named [Wyam.Tool](https://www.nuget.org/packages/Wyam.Tool/) and you can install it like this:

```
dotnet tool install -g Wyam.Tool
```

Then you can use it with the `wyam` command:

```
wyam ...
```

## ZIP Archive

The ZIP archive contains a .NET Core [framework-dependent deployment](https://docs.microsoft.com/en-us/dotnet/core/deploying/#framework-dependent-deployments-fdd), which means it's shipped as a DLL assembly that requires the `dotnet` CLI bootstrapper to run. Once you download the [ZIP archive](https://github.com/Wyamio/Wyam/releases) and extract it somewhere, running Wyam looks like:

```
dotnet /path/to/wyam/Wyam.dll ...
```

## Cake

If you're using Cake and the Cake addin, nothing will change (other than the requirement for having .NET Core installed).

# Looking Ahead

A couple things will change going forward. The first is that I'll start blogging about Wyam releases and other topics here. That's in response to some [very valid criticism](https://github.com/Wyamio/Wyam/issues/741) about lack of communication and frequency of breaking changes. While I'm pretty sure more breaking changes are on the horizon, I could certainly do better about detailing what they are and how to migrate.

## Near Term

Some bugs, particularly regarding the docs recipe, have surfaced recently while I've been focusing on .NET Core. In the near future I'll be working on squashing those and making small enhancements.

## Long Term

Long term, I'm going to start working on our [vNext client strategy](https://github.com/Wyamio/Wyam/issues/668). This was originally intended to ship with .NET Core support in 2.0 but as we got closer, I decided it was better to ship a 2.0 release that looks exactly like 1.x and then do bigger architecture evolution in the next major release. I suggest you read [the associated issue](https://github.com/Wyamio/Wyam/issues/668) since vNext will look very different than the current version.