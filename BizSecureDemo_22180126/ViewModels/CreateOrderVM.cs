using System.ComponentModel.DataAnnotations;
namespace BizSecureDemo_22180126.ViewModels;
public class CreateOrderVm
{
    // Extended the limit on purpose for the XSS attack
    //[Required, MaxLength(300)]

    [Required, MaxLength(80)]
    public string Title { get; set; } = "";
    [Required]
    public decimal Amount { get; set; }
}
