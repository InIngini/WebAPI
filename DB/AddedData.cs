using DB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Guide;

namespace DB
{
    public class AddedData
    {
        public AddedData()
        {
            using (var context = new Context())
            {
                var blocks = new List<NumberBlock>();
                blocks.Add(new NumberBlock
                {
                    Name = "Личность"
                });
                blocks.Add(new NumberBlock
                {
                    Name = "Внешность"
                });
                blocks.Add(new NumberBlock
                {
                    Name = "Характер"
                });
                blocks.Add(new NumberBlock
                {
                    Name = "По истории"
                });
                context.NumberBlocks.AddRange(blocks);
                context.SaveChanges();

                var questions = new List<Question>();
                questions.Add(new Question
                {
                    Name = "ФИО",
                    Block = 1,
                });
                questions.Add(new Question
                {
                    Name = "Прозвище",
                    Block = 1,
                });
                questions.Add(new Question
                {
                    Name = "Дата рождения",
                    Block = 1,
                });
                questions.Add(new Question
                {
                    Name = "Знак зодиака",
                    Block = 1,
                });
                questions.Add(new Question
                {
                    Name = "Пол",
                    Block = 1,
                });
                questions.Add(new Question
                {
                    Name = "Ориентация",
                    Block = 1,
                });
                questions.Add(new Question
                {
                    Name = "Биологический вид",
                    Block = 1,
                });
                questions.Add(new Question
                {
                    Name = "Цвет глаз",
                    Block = 2,
                });
                questions.Add(new Question
                {
                    Name = "Цвет волос",
                    Block = 2,
                });
                questions.Add(new Question
                {
                    Name = "Рост",
                    Block = 2,
                });
                questions.Add(new Question
                {
                    Name = "Телосложение",
                    Block = 2,
                });
                questions.Add(new Question
                {
                    Name = "Шрамы",
                    Block = 2,
                });
                questions.Add(new Question
                {
                    Name = "Тату",
                    Block = 2,
                });
                questions.Add(new Question
                {
                    Name = "Любимая одежда",
                    Block = 2,
                });
                questions.Add(new Question
                {
                    Name = "Необычные элементы в одежде",
                    Block = 2,
                });
                questions.Add(new Question
                {
                    Name = "Прическа",
                    Block = 2,
                });
                questions.Add(new Question
                {
                    Name = "Тип личности",
                    Block = 3,
                });
                questions.Add(new Question
                {
                    Name = "Самооценка",
                    Block = 3,
                });
                questions.Add(new Question
                {
                    Name = "Привычки",
                    Block = 3,
                });
                questions.Add(new Question
                {
                    Name = "Принципы",
                    Block = 3,
                });
                questions.Add(new Question
                {
                    Name = "Интересы",
                    Block = 3,
                });
                questions.Add(new Question
                {
                    Name = "Убеждения",
                    Block = 3,
                });
                questions.Add(new Question
                {
                    Name = "Цели",
                    Block = 3,
                });
                questions.Add(new Question
                {
                    Name = "Проблема в начале истории",
                    Block = 4,
                });
                questions.Add(new Question
                {
                    Name = "Цель на протяжении истории",
                    Block = 4,
                });
                questions.Add(new Question
                {
                    Name = "Изменения в характере на протяжении истории",
                    Block = 4,
                });
                questions.Add(new Question
                {
                    Name = "Ожидаемая реакция читателей",
                    Block = 4,
                });
                questions.Add(new Question
                {
                    Name = "Причина интереса читателей",
                    Block = 4,
                });

                // Добавление значений в контекст данных
                context.Questions.AddRange(questions);
                context.SaveChanges();

                var sexs = new List<Sex>();
                sexs.Add(new Sex
                { Name = "Ж" });
                sexs.Add(new Sex
                { Name = "М" });
                // Добавление значений в контекст данных
                context.Sex.AddRange(sexs);
                context.SaveChanges();

                var typesBelong = new List<TypeBelongToBook>();
                typesBelong.Add(new TypeBelongToBook
                { Name = "Автор" });
                typesBelong.Add(new TypeBelongToBook
                { Name = "Соавтор" });
                // Добавление значений в контекст данных
                context.TypeBelongToBooks.AddRange(typesBelong);
                context.SaveChanges();

                var typesConnection = new List<TypeConnection>();
                typesConnection.Add(new TypeConnection
                { Name = "Партнер" });
                typesConnection.Add(new TypeConnection
                { Name = "Ребенок-родитель" });
                typesConnection.Add(new TypeConnection
                { Name = "Сиблинг" });
                // Добавление значений в контекст данных
                context.TypeConnections.AddRange(typesConnection);
                context.SaveChanges();
            }
        }
    }
}
