namespace GiphyCli
{
    using System.ComponentModel.DataAnnotations;

    internal enum ActionSelectOptions
    {
        [Display(Name = "Open Giphy.com")]
        OpenGiphyCom,

        [Display(Name = "Copy gif URL to clipboard")]
        CopyUrl,

        [Display(Name = "Copy markdown to clipboard")]
        CopyMarkdown,

        [Display(Name ="Exit")]
        Exit,
    }
}
