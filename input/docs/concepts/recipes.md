Title: Recipes
Description: A recipe is a pre-configured series of modules and pipelines.
Order: 6
---
A *recipe* is a pre-configured series of modules and pipelines. <a href="/recipes">Each recipe</a> can be thought of as it's own special purpose static site generator. For example, the <a href="/recipes/blog">Blog</a> recipe can be thought of as analogous to a tool like Jekyll. Recipes can still make use of <a href="/docs/usage/configuration">configuration files</a> to further tweak the generation process, but they aren't required. 

# Pipelines

The pipelines and other content from a recipe is loaded before your <a href="/docs/usage/configuration">configuration file</a>. This makes it possible to change the behavior of the recipe in your own configuration file. For example, you can prevent a particular pipeline in the recipe from running by clearing it (though you can't remove pipelines):

```
Pipelines["Pages"].Clear();
```

You can also substitute your own logic for a recipe-provided pipeline:

```
Pipelines["Pages"].Clear();
Pipelines["Pages"].Add(
    ReadFiles("**/*.md"),
    // ... more modules
);
```

There's also no reason why you can't add your own pipelines to a recipe to implement some additional custom behavior. Combining a recipe with your own pipelines and modules is a very powerful way to customize your generation without having to start from scratch. You can even use `IPipelineCollection.InsertBefore()` and `IPipelineCollection.InsertAfter()` overloads to control where in the generation process you want your custom pipelines to run relative to those from the recipe.