Title: Paginate
Description: Partitions a sequence of documents into equally sized pages.
Category: Control
---
This module takes a sequence of documents and splits it into multiple pages. Each input document is then cloned for each page and metadata related to the pages, including the sequence of documents for each page, is added to each clone. For example, if your input document is a [Razor](/modules/razor) template for a blog archive, you can use Paginate to get pages of 10 blog posts each. If you have 50 blog posts, the result of the Paginate module will be 5 copies of your input archive template, one for each page. Your configuration file might look something like this:

```
Pipelines.Add("Posts",
	ReadFiles("*.md"),
	Markdown(),
	WriteFiles("html")
);

Pipelines.Add("Archive",
	ReadFiles("archive.cshtml"),
	Paginate(10,
		Documents("Posts")	
	),
	Razor(),
	WriteFiles(string.Format("archive-{0}.html", @@doc["CurrentPage"]))
);
```

# Usage
---

  - `Paginate(int pageSize, params IModule[] modules)`
  
    Partitions the result of the specified modules into `pageSize` number of pages. The input documents to Paginate are used as the initial input documents to the specified modules.
	
# Metadata
---

The following metadata is added to each document.

  - `PageDocuments`
  
    An `IEnumerable<IDocument>` containing all the documents for the current page.
	
  - `CurrentPage`
  
    The index of the current page (1-based).
	
  - `TotalPages`
  
    The total number of pages.
	
  - `HasNextPage`
  
    Whether there is another page after this one.
	
  - `HasPreviousPage`
  
    Whether there is another page before this one.