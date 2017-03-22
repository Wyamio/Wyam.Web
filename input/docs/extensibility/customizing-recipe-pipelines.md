Title: Customizing Recipe Pipelines
Description: Tips for customizing recipes by modifying their pipelines and modules.
---
The [official recipes](/recipes) are designed to work without modification for the most common scenarios. However, there may be times when you want to customize the behavior of a [recipe](/docs/concepts/recipes) without totally recreating all of it's [pipelines](/docs/concepts/pipelines) and [modules](/docs/concepts/modules). Thankfully there are several ways to remove, replace, or modify recipe pipelines. Alternatively (or in addition) you can also look at [customizing recipe themes](/docs/extensibility/customizing-recipe-themes).

# Customizing Pipelines

Every pipeline in a recipe has a name. You can see a list of the pipelines for a recipe and their names in the documentation for each recipe (for example, [here are the pipelines for the blog recipe](/recipes/blog/pipelines)). Once you know the name of a pipeline you can manipulate it in several ways. Given a pipeline with the name "Name", place the following code snippets in your [configuration file](/docs/usage/configuration).

## Remove A Pipeline

The following code will completely remove the "Name" pipeline from the recipe.

```
Pipelines.Remove("Name");
```

## Replace A Pipeline

The easiest way to replace a pipeline is to clear all of it's modules and then add new ones.

```
Pipelines["Name"].Clear();
Pipelines["Name"].AddRange(
    ModuleA(),
    ModuleB()
);
```

## Add A New Pipeline

You may need to introduce entirely new behavior to the recipe by adding a new pipeline.

```
Pipelines.Add("New",
    ModuleA(),
    ModuleB()
);
```

## Insert A New Pipeline

Likewise, you may need to place your new pipeline in a specific order within the other pipelines in the recipe. You can fine-tune exactly where to insert the pipeline in relation to the other named pipelines.

```
Pipelines.InsertAfter("Name", "New",
    ModuleA(),
    ModuleB()
);
```

# Customizing Modules

In addition to customizing entire pipelines, you can also customize individual modules by adding, inserting, or removing them from recipe pipelines. Some modules are named as shown on the recipe pipelines documentation for each recipe. If a target module is named you can use it's name in the following code. Otherwise you'll need to specify modules for modification by type or index.

## Remove A Module

To remove a named module, pass the module name to the remove method.

```
Pipelines["PipelineName"].Remove("ModuleName");
```

Alternatively, you can remove a module at a specific index.

```
Pipelines["PipelineName"].RemoveAt(3);
```

## Insert Or Replace A Module

You can insert or replace modules in a pipeline by using the extension methods in `ModuleListExtensions`. These extensions support insertion and removal of modules by specifying a target module name, type, or index.
