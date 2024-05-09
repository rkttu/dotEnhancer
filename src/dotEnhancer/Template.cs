/*
Using templates for code contributors

You can copy this template file to add a new category of extension method series.

- Name your new static class, and replace "Template" with your new name.
- Rename the copied C# code file to match the name of your new class.
- Put all code, including using statements, between each #if to #endif block.
- Delete any unused conditional blocks.
- After copying the template code, remove this comment block.
- When you're ready to ship, change the access restriction of the class at the very end of the file from 'internal' to 'public.'

Notes:

- InternalsVisibleTo property is granted for all .NET Standard projects.
- The unit test project is set up to use the highest version of .NET Standard, so you should see the extension method classes no matter what conditions you apply.
*/

namespace dotEnhancer
{
#if NETSTANDARD1_0_OR_GREATER
    partial class Template { }
#endif // NETSTANDARD1_0_OR_GREATER

#if NETSTANDARD1_1_OR_GREATER
    partial class Template { }
#endif // NETSTANDARD1_1_OR_GREATER

#if NETSTANDARD1_2_OR_GREATER
    partial class Template { }
#endif // NETSTANDARD1_2_OR_GREATER

#if NETSTANDARD1_3_OR_GREATER
    partial class Template { }
#endif // NETSTANDARD1_3_OR_GREATER

#if NETSTANDARD1_4_OR_GREATER
    partial class Template { }
#endif // NETSTANDARD1_4_OR_GREATER

#if NETSTANDARD1_5_OR_GREATER
    partial class Template { }
#endif // NETSTANDARD1_5_OR_GREATER

#if NETSTANDARD1_6_OR_GREATER
    partial class Template { }
#endif // NETSTANDARD1_6_OR_GREATER

#if NETSTANDARD2_0_OR_GREATER
    partial class Template { }
#endif // NETSTANDARD2_0_OR_GREATER

#if NETSTANDARD2_1_OR_GREATER
    partial class Template { }
#endif // NETSTANDARD2_1_OR_GREATER

    internal static partial class Template { }
}
