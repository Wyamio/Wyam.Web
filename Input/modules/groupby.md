Title: GroupBy
Description: Partitions a sequence of documents based on a key.
Category: Control
---
This module takes a sequence of documents and splits it into groups based on a specified key function. This module works similarly to [Paginate](/modules/paginate).

# Usage
---

  - `GroupBy(DocumentConfig key, params IModule[] modules)`
  
    Partitions the result of the specified modules into groups with matching keys based on the `key` delegate. The input documents to GroupBy are used as the initial input documents to the specified modules.
	
# Metadata
---

The following metadata is added to each document.

  - `GroupDocuments`
  
    An `IEnumerable<IDocument>` containing all the documents for the current group.
	
  - `GroupKey`
  
    The key for the current group.