using AppData.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AppView.PhanTrang
{
    public class PhanTrangBase<T> where T : class
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
