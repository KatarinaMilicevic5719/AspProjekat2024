using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.API.Core;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.Queries;
using Asp.DataAccess;
using Asp.Domain;
using Asp.Implementation.UseCases.Commands;
using Asp.Implementation.UseCases.Queries.EF;
using Asp.Implementation.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Asp.API.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddTransient(x =>
            {
                var context = x.GetService<AspDbContext>();
                var settings = x.GetService<AppSettings>();

                return new JWTMenager(context, settings.JwtSettings);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg => 
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.JwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IGetCategoriesQuery, EfGetCategoriesQuery>();
            services.AddTransient<IGetPostsQuery, EfGetPostsQuery>();
            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<IConnectFriendsCommand, EfConnectFriendsCommand>();
            services.AddTransient<ICreatePostCommand, EfCreatePostCommand>();
            services.AddTransient<IDeleteCommand, EfDeleteCategoryCommand>();
            services.AddTransient<IModifiedCategoryCommand, EfModifiedCategoryCommand>();
            services.AddTransient<ISendMessageCommand, EfSendMessageCommand>();
            services.AddTransient<IModifiedUserUseCasesCommand, EfModiefiedUserUseCaseCommand>();
            services.AddTransient<IAddLikeCommand, EfAddLikeCommand>();
            services.AddTransient<IDislikeCommand, EfDislikeCommand>();
            services.AddTransient<IGetSuggestFriendQuery, EfSuggestFriendQuery>();
            services.AddTransient<ICreateCommentCommand, EfCreateCommentCommand>();
            services.AddTransient<IEditCommentCommand, EfEditCommentCommand>();
            services.AddTransient<IDeleteCommand, EfDeleteCommentCommand>();
            services.AddTransient<IDeleteCommand, EfDisconnectFriendsCommand>();
            services.AddTransient<IGetMessagesQuery, EfGetMessagesQuery>();
            services.AddTransient<IGetFriendMessagesQuery, EfGetFriendMessagesQuery>();
            services.AddTransient<IEditMessageCommand, EfEditMessageCommand>();
            services.AddTransient<IDeleteMessageCommand, EfDeleteMessageCommand>();
            services.AddTransient<IGetOnePostQuery, EfGetOnePostQuery>();
            services.AddTransient<IDeletePostCommand, EfDeletePostCommand>();
            services.AddTransient<IModifiePostCommand, EfEditPostCommand>();
            services.AddTransient<IGetAuditLogQuery, EfGetAuditLogQuery>();
            services.AddTransient<IGetUserProfileQuery, EfGetUserProfileQuery>();
            services.AddTransient<IEditProfileCommand, EfEditUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<CategoryValidator>();
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<ConnectFriendsValidator>();
            services.AddTransient<CreatePostValidator>();
            services.AddTransient<DeleteCategoryValidator>();
            services.AddTransient<ModifiedCategoryValidator>();
            services.AddTransient<SendMessageValidator>();
            services.AddTransient<ModifiedUserUseCaseValidator>();
            services.AddTransient<CommentValidator>();
            services.AddTransient<EditCommentValidator>();
            services.AddTransient<EditPostValidator>();
            services.AddTransient<EditUserValidator>();
            

        }

        public static void AddApplicationUser(this IServiceCollection services)
        {
            services.AddTransient<IApplicationUser>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var header = accessor.HttpContext.Request.Headers["Authorization"];

                var claims = accessor.HttpContext.User;

                if(claims == null || claims.FindFirst("UserId") == null) 
                {
                    return new AnonymousUser();
                }

                var actor = new JWTUser
                {
                    Identity = claims.FindFirst("Email").Value,
                    Id = Int32.Parse(claims.FindFirst("UserId").Value),
                    Email = claims.FindFirst("Email").Value,
                    UseCaseIds = JsonConvert.DeserializeObject<List<int>>(claims.FindFirst("UseCases").Value)
                };
                Console.WriteLine(JsonConvert.DeserializeObject<List<int>>(claims.FindFirst("UseCases").Value));
                return actor;

            });
        }

        public static void AddAspDbContext(this IServiceCollection services)
        {
            services.AddTransient<AspDbContext>(x =>
            {
                var optionBuilder = new DbContextOptionsBuilder();

                var conString = x.GetService<AppSettings>().ConString;

                optionBuilder.UseSqlServer(conString).UseLazyLoadingProxies();

                var options = optionBuilder.Options;

                return new AspDbContext(options);
            });
        }
    }
}
