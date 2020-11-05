using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using QWA.Models;

namespace KrutQuest.Service.Data
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            context.TechUsers.Add(new TechUser
            {
                Id = Guid.NewGuid(),
                Login = "Admin",
                Password = "Admin2019"
            });

            var quest = new Quest
            {
                Id = Guid.NewGuid(),
                Name = "Квест",
                RulesText = "1. Первое правило бойцовского клуба - никто не должен знать о бойцовском клубе.",
                FinishText = "ЭТО УСПЕХ!",
                Duration = 120
            };

            context.Quests.Add(quest);

            context.Teams.Add(new Team
            {
                Id = Guid.NewGuid(),
                Login = "team",
                Password = "qwerty",
                Name = "Команда",
                QuestId = quest.Id,
                Quest = quest
            });
        }
    }
}