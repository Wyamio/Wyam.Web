Title: Concepts
Description: Describes the primary concepts of documents, modules, and pipelines.
Order: 1
---
The primary concepts in Wyam are *documents*, *modules*, and *pipelines*. 

# Documents
---

A *document* is a combination of *content* and *metadata* as it makes it's way through the system. The content of a document is what most modules manipulate and is what you will presumably output at the end of the pipeline. The metadata serves as a way to pass information to and from modules to other modules. Once a value is added to the metadata by one module, it can never be removed by a subsequent one (though it can be overwritten). It can be important to note that documents are immutable. Though we often talk about documents being "transformed" or "manipulated" by modules, this isn't strictly accurate. Instead modules return a new copy of the document with different content and/or additional metadata.

# Modules
---

A *module* is a small single-purpose component that takes in documents, does something based on those documents (possibly transforming them), and outputs documents as a result of whatever operation was performed.

# Pipelines
---

A *pipeline* is a series of modules executed in sequence that results in final output documents. A given Wyam configuration can have multiple pipelines which are executed in sequence, and subsequent pipelines have access to the documents from the previous pipelines.

A simple pipeline looks like:
```
  [Empty Document]
         |
      [Module]
	  /      \
[Document] [Document]
    |           |
  [----Module*----]
    |           |
[Document] [Document]

* modules often (though not always) handle documents in parallel for performance
```

In the visualization above, the first module may have read some files (in this case 2 files) and stuck some information about those files such as name and path in the document metadata. Then the second module may have transformed the files (for example, from Markdown to HTML).