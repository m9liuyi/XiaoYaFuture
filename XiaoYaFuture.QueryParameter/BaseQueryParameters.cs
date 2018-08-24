using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XiaoYaFuture.QueryParameter
{

    /// <summary>
    /// 1. 子类中所有属性必须可空
    /// 2. 见Extension.QueryToWhere方法
    /// 3.1 字符串：已Equals/Contains结尾
    /// 3.2 long/int/double/decimal/DateTime: 若是区间以Min/Begin结尾表示下限值，以Max/End结尾表示上限值；不以上述结尾则直接用“==”
    /// 3.3 List<T>: 表示包含
    /// </summary>
    public class BaseQueryParameters
    {
        /// <summary>
        /// 以无意义主键作为查询条件的查询，在Extension.QueryToWhere方法中会转为对应Entity的主键, 即拥有KeyAttribute的属性”
        /// </summary>
        public int? PrimaryKey { get; set; }

        public List<int> PrimaryKeys { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int? PageSize { get; set; }


        /// <summary>
        /// 总页数
        /// </summary>
        public int? PageCount { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int? CurrentPage { get; set; }
    }


}
