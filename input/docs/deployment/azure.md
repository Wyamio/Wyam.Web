Description: How to deploy your static site to Azure.
---
Deploying to Azure is a really simple way to get up and running.

The first step is to set up a new *Web App* which can be found under *App Services* in the new Azure portal. Once you have the web app set up and configured, you can push your files to Azure in a number of ways. The exact mechanism isn't really important, but I've found SFTP works well. You can find your SFTP login information on the *Overview* page for your new web app. Just use any FTP application (like [FileZilla](https://filezilla-project.org/)) to connect to the given server using the given credentials. Once you're connected, upload the contents of the Wyam `output` folder to the `/site/wwwroot` folder in your Azure site (don't upload the Wyam `output` folder itself, just everything that's underneath it).

One note is that you'll probably want support for extensionless URLs like most other static site hosts. Azure and IIS don't support this by default, but you can configure it using the following `web.config` file:

```
<configuration>
  <system.webServer>
    <rewrite>
      <rules>
        <rule name="html">
          <match url="(.*)" />
          <conditions>
            <add input="{REQUEST_FILENAME}" matchType="IsFile" negate="true" />
            <add input="{REQUEST_FILENAME}" matchType="IsDirectory" negate="true" />
          </conditions>
          <action type="Rewrite" url="{R:1}.html" />
        </rule>
      </rules>
    </rewrite>
  </system.webServer>
</configuration>
 ```

 You can either upload this file to your `/site/wwwroot` folder separately or place it in your Wyam `input` folder so it always gets copied to the `output` folder for upload. A more detailed examination of `web.config` files for static sites can be found [at this blog post](http://andyhansen.co.nz/posts/web-config-for-a-static-site).

 If you want to automate the upload process, you can consider using [Kudu](https://github.com/projectkudu/kudu/wiki/Deployment) or [read this blog post](https://daveaglick.com/posts/synchronizing-files-with-azure-web-apps-over-ftp) for an approach that uses FTP.