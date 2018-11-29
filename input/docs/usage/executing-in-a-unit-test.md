Title: Executing In A Unit Test
Description: How to call Wyam from within a unit test.
Order: 6
RedirectFrom: knowledgebase/executing-in-a-unit-test
---
There may be times when you want to call Wyam from a unit test (or other bit of code) without actually [embedding it](/docs/usage/embedding). Here's an example NUnit unit test that does this (though the technique should work for any test framework):

```
[Test]
public void ExecuteExample()
{
    string rootPath = "[your root path here]";

    // Note that you may need to use a different exe path depending 
    // on where you got Wyam from (I.e., from the tools package)
    string wyamPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Wyam.dll");

    string arguments = "--preview"; // or whatever as appropriate

    Process process = new Process();
    process.StartInfo.FileName = "dotnet";
    process.StartInfo.Arguments = $"{wyamPath} {rootPath} {arguments}";
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