Title: Executing In A Unit Test
Description: How to call wyam.exe from within a unit test.
---
There may be times when you want to call wyam.exe from a unit test (or other bit of code) without actually [embedding it](/knowledgebase/embedded-use). Here's an example NUnit unit test that does this (though the technique should work for any test framework):

```
[Test]
public void ExecuteExample()
{
    string rootPath = "[your root path here]";
    string exePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Wyam.exe");
    // Note that you may need to use a different exe path depending 
    // on where you got Wyam from (I.e., from the tools package)

    Process process = new Process();
    process.StartInfo.FileName = exePath;
    process.StartInfo.Arguments = rootPath;
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.UseShellExecute = false;
    process.StartInfo.RedirectStandardOutput = true;
    process.StartInfo.RedirectStandardError = true;
    process.OutputDataReceived += (s, e) => TestContext.Out.WriteLine(e.Data);
    process.ErrorDataReceived += (s, e) => TestContext.Out.WriteLine(e.Data);
    process.Start();
    process.BeginOutputReadLine();
    process.WaitForExit();
    Assert.AreEqual(0, process.ExitCode);
}
```