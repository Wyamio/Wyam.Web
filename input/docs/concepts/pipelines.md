Title: Pipelines
Description: A pipeline is a series of modules executed in sequence.
Order: 4
---
A *pipeline* is a series of modules executed in sequence that results in final output documents. A given Wyam configuration can have multiple pipelines which are executed in sequence, and subsequent pipelines have access to the documents from the previous pipelines.

Conceptually, a simple pipeline looks like:

<div class="mermaid">
    graph TD
        D1("Empty Document")
        D1-->Module1["Module 1"]
        Module1-->D2("Document A")
        Module1-->D3("Document B")
        D2-->Module2["Module 2"]
        D3-->Module2
        Module2-->D4("Document C")
        Module2-->D5("Document D")
</div>

In the visualization above, the first module may have read some files (in this case 2 files) and stuck some information about those files such as name and path in the document metadata. Then the second module may have transformed the files (for example, from Markdown to HTML).

It's not unusual for a real-world generation to contain many different pipelines. Many times this is helpful if you need to reuse the output from one of the pipelines or want to separate the different generation steps.

<div class="mermaid">
    graph TD
        subgraph Second Pipeline
            D6("Empty Document")
            D6-->Module3["Module 3"]
            Module3-->D7("Document E")
            D7-->Module4["Module 4"]
            Module4-->D8("Document F")
        end
        subgraph First Pipeline
            D1("Empty Document")
            D1-->Module1["Module 1"]
            Module1-->D2("Document A")
            Module1-->D3("Document B")
            D2-->Module2["Module 2"]
            D3-->Module2
            Module2-->D4("Document C")
            Module2-->D5("Document D")
        end
</div>