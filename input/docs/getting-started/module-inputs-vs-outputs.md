Title: Module Inputs vs. Outputs
Description: A discussion about different input vs. output behavior.
Order: 50
---
Modules in Wyam treat inputs and outputs in different ways. Some modules just "pass-through" the documents that are input, some transform them in some way and output the results, and some exhibit more complex behavior. Some modules even exhibit different behavior depending on how the module was configured. The behavior can get especially confusing when considering some modules evaluate child modules which also output documents. In these cases, there are different behaviors for how the input documents and the result documents from the child modules are combined. The way inputs, outputs, and child module results are related can generally be described as a few different patterns and even though these probably don't cover the way *every* module works, they should help you understand the concepts involved.

# <a name="pass-through"></a>Pass-Through

These modules just take the input documents and pass them on as the outputs:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->O1(I1)
        Module-->O2(I2)
</div>

# <a name="transformation"></a>Transformation

These modules apply some sort of transformation to the input documents and output one result for each input:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->O1("O1 (from I1)")
        Module-->O2("O2 (from I2)")
</div>

# <a name="aggregation"></a>Aggregation

These modules take multiple input documents and combine them into a single output document:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->O1("O1 (from I1 + I2)")
</div>

# <a name="splitting"></a>Splitting

This is the opposite behavior of aggregation. These modules split each input document into multiple output documents:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->O1A("O1A (from I1)")
        Module-->O1B("O1B (from I1)")
        Module-->O2A("O2A (from I2)")
        Module-->O2B("O2B (from I2)")
</div>

# <a name="concatenation"></a>Concatenation

In this case, output documents that are independent from the input documents are output, but instead of replacing the input document they are concatenated with them:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->O1(I1)
        Module-->O2(I2)
        Module-->O3(O1)
        Module-->O4(O2)
</div>

Note that the new documents may come from a sequence of child modules:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->Children["Module(s)"]
        subgraph Child Modules
            Children-->C1(C1)
            Children-->C2(C2)
        end
        Module-->O1(I1)
        Module-->O2(I2)
        Module-->O3(C1)
        Module-->O4(C2)
</div>

# <a name="replacement"></a>Replacement

These modules just replace the entire input set of documents with a different output set:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->O1(O1)
        Module-->O2(O2)
</div>

Note that the new documents may come from a sequence of child modules:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->Children["Module(s)"]
        subgraph Child Modules
            Children-->C1(C1)
            Children-->C2(C2)
        end
        Module-->O1(C1)
        Module-->O2(C2)
</div>

Further, some modules support a `ForEachDocument()` method that runs the entire set of child modules *for each input document*:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->Children1["Module(s)"]
        Module-->Children2["Module(s)"]
        subgraph Child Modules with I2
            Children2-->C2A(C2A)
            Children2-->C2B(C2B)
        end
        subgraph Child Modules with I1
            Children1-->C1A(C1A)
            Children1-->C1B(C1B)
        end
        Module-->O1(C1A)
        Module-->O2(C1B)
        Module-->O3(C2A)
        Module-->O4(C2B)
</div>

# <a name="combination"></a>Combination

This pattern describes the combination of one or more input documents with the outputs from child modules:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->Children["Module(s)"]
        subgraph Child Modules
            Children-->C1(C1)
            Children-->C2(C2)
        end
        Module-->O1("O1 (from I1 + C1)")
        Module-->O2("O2 (from I1 + C2)")
        Module-->O3("O3 (from I2 + C1)")
        Module-->O4("O4 (from I2 + C2)")
</div>

As described above, some modules support a `ForEachDocument()` method that runs the entire set of child modules *for each input document*:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->Children1["Module(s)"]
        Module-->Children2["Module(s)"]
        subgraph Child Modules with I2
            Children2-->C2A(C2A)
            Children2-->C2B(C2B)
        end
        subgraph Child Modules with I1
            Children1-->C1A(C1A)
            Children1-->C1B(C1B)
        end
        Module-->O1("O1 (from I1 + C1A)")
        Module-->O2("O2 (from I1 + C1B)")
        Module-->O3("O3 (from I2 + C2A)")
        Module-->O4("O4 (from I2 + C2B)")
</div>