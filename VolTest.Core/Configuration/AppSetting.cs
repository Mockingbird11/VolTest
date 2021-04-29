using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using VolTest.Core.Const;
using VolTest.Core.Extensions;

namespace VolTest.Core.Configuration
{
    public class AppSetting
    {
        public static IConfiguration Configuration { get; private set; }
        private static Connection _connection;
        public static string DbConnectionString
        {
            get { return _connection.DbConnectionString; }
        }

        public static string RedisConnectionString
        {
            get { return _connection.RedisConnectionString; }
        }

        public static bool UseRedis
        {
            get { return _connection.UseRedis; }
        }

        public static Secret Secret { get; private set; }

        public static CreateMember CreateMember { get; private set; }

        public static CreateMember ModifyMember { get; private set; }

        public static string TokenHeaderName = "Authorization";

        /// <summary>
        /// Actions权限过滤
        /// </summary>
        public static GlobalFilter GlobalFilter { get; set; }

        /// <summary>
        /// JWT有效期（分钟，默认120）
        /// </summary>
        public static int ExpMinutes { get; private set; } = 120;

        public static string CurrentPath { get; private set; } = null;

        public static string DownLoadPath { get => CurrentPath + "\\DownLoad\\"; }

        public static void Init(IServiceCollection services, IConfiguration configuration)
        {
            Configuration = configuration;
            services.Configure<Secret>(configuration.GetSection("Secret"));
            services.Configure<Connection>(configuration.GetSection("Connection"));
            services.Configure<CreateMember>(configuration.GetSection("CreateMember"));
            services.Configure<ModifyMember>(configuration.GetSection("ModifyMember"));
            services.Configure<GlobalFilter>(configuration.GetSection("GlobalFilter"));

            var provider = services.BuildServiceProvider();
            IWebHostEnvironment environment = provider.GetRequiredService<IWebHostEnvironment>();
            CurrentPath = Path.Combine(environment.ContentRootPath, "").ReplacePath();

        }
    }

    public class Connection
    {
        public string DBType { get; set; }
        public string DbConnectionString { get; set; }
        public string RedisConnectionString { get; set; }
        public bool UseRedis { get; set; }

    }

    public class CreateMember : TableDefaultColumns
    {

    }

    public class ModifyMember : TableDefaultColumns
    {

    }

    public abstract class TableDefaultColumns
    {
        public string UserIdField { get; set; }
        public string UserNameField { get; set; }
        public string DateField { get; set; }

    }

    public class GlobalFilter
    {
        public string Message { get; set; }
        public bool Enable { get; set; }
        public string[] Actions { get; set; }
    }
}
