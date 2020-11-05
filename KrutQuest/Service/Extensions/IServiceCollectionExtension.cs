using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using KrutQuest.Service.Repositories.Abstract;
using KrutQuest.Service.Repositories.Concrete;

namespace KrutQuest.Service.Extensions
{
    /// <summary>
    /// Расширения для IServiceCollection
    /// </summary>
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// Внедряет зависимости репозиториев в IServiceCollection
        /// </summary>
        /// <param name="services">Экземпляр IServiceCollection</param>
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ITechUserRepository, TechUserRepository>();
            services.AddScoped<IQuestRepository, QuestRepository>();
            services.AddScoped<IQuestionGroupRepository, QuestionGroupRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ITeamAnswerRepository, TeamAnswerRepository>();
            services.AddScoped<IServerImageRepository, ServerImageRepository>();
        }
    }
}
