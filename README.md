## 结构框架

![image](https://github.com/m9liuyi/NiceNet/raw/master/upload/images/framework.png)

## 依赖注入
~\NiceNet\App_Start\AutofacConfig.cs 中统一属性注入，使用时如下
```c#
    public class UserController : ApiController
    {
        public IUserManager UserManager { get; set; }

        /// <summary>
        /// 根据 query 查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("List")]
        public List<DTO_XYFUser> Get([FromUri]UserQueryParameters query)
        {
            return this.UserManager.List(query ?? new UserQueryParameters());
        }
    }
```

## [Frontend Master](https://frontendmasters.com/books/front-end-handbook/2018/)
