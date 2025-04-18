﻿using WebAPI.DB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Guide;

namespace WebAPI.DB
{
    /// <summary>
    /// Класс для добавления начальных данных в контекст базы данных.
    /// </summary>
    public class AddedData
    {
        private readonly IContext Context;

        public AddedData(IContext context)
        {
            Context = context; // Сохранение контекста для использования в методах
        }

        /// <summary>
        /// Метод для инициализации данных.
        /// </summary>
        public async Task Initialize()
        {
            if (!Context.NumberBlocks.Any())
            {
                // Инициализация блоков
                var blocks = new List<NumberBlock>
                {
                    new NumberBlock { Name = "Личность" },
                    new NumberBlock { Name = "Внешность" },
                    new NumberBlock { Name = "Характер" },
                    new NumberBlock { Name = "По истории" }
                };
                // Добавление блоков в контекст и сохранение изменений
                Context.NumberBlocks.AddRange(blocks);
                await Context.SaveChangesAsync();
            }

            if (!Context.Questions.Any())
            {
                // Инициализация вопросов
                var questions = new List<Question>
                {
                    new Question { QuestionText = "Прозвище", Block = 1 },
                    new Question { QuestionText = "Дата рождения", Block = 1 },
                    new Question { QuestionText = "Знак зодиака", Block = 1 },
                    new Question { QuestionText = "Пол", Block = 1 },
                    new Question { QuestionText = "Ориентация", Block = 1 },
                    new Question { QuestionText = "Биологический вид", Block = 1 },
                    new Question { QuestionText = "Цвет глаз", Block = 2 },
                    new Question { QuestionText = "Цвет волос", Block = 2 },
                    new Question { QuestionText = "Рост", Block = 2 },
                    new Question { QuestionText = "Телосложение", Block = 2 },
                    new Question { QuestionText = "Шрамы", Block = 2 },
                    new Question { QuestionText = "Тату", Block = 2 },
                    new Question { QuestionText = "Любимая одежда", Block = 2 },
                    new Question { QuestionText = "Необычные элементы в одежде", Block = 2 },
                    new Question { QuestionText = "Прическа", Block = 2 },
                    new Question { QuestionText = "Интроверт или экстраверт?", Block = 3 },
                    new Question { QuestionText = "Флегматичен или меланхоличен?", Block = 3 },
                    new Question { QuestionText = "Самооценка", Block = 3 },
                    new Question { QuestionText = "Привычки", Block = 3 },
                    new Question { QuestionText = "Принципы", Block = 3 },
                    new Question { QuestionText = "Что любит?", Block = 3 },
                    new Question { QuestionText = "Что не любит?", Block = 3 },
                    new Question { QuestionText = "Чего боится?", Block = 3 },
                    new Question { QuestionText = "О чем мечтает?", Block = 3 },
                    new Question { QuestionText = "В чем заключается его проблема в начале истории?", Block = 4 },
                    new Question { QuestionText = "К чему стремится на протяжении истории?", Block = 4 },
                    new Question { QuestionText = "Как персонаж меняется на протяжении истории?", Block = 4 },
                    new Question { QuestionText = "Как читатели должны реагировать на этого персонажа?", Block = 4 },
                    new Question { QuestionText = "Почему персонаж должен быть им интересен?", Block = 4 },
                };

                // Добавление вопросов в контекст и сохранение изменений
                Context.Questions.AddRange(questions);
                await Context.SaveChangesAsync();
            }

            if (!Context.Sex.Any())
            {
                // Инициализация типов сексуальной принадлежности
                var sexs = new List<Sex>
                {
                    new Sex { Name = "Ж" },
                    new Sex { Name = "М" }
                };

                // Добавление типов сексуальной принадлежности в контекст и сохранение изменений
                Context.Sex.AddRange(sexs);
                await Context.SaveChangesAsync();
            }

            if (!Context.TypeBelongToBooks.Any())
            {
                // Инициализация типов принадлежности к книге
                var typesBelong = new List<TypeBelongToBook>
                {
                    new TypeBelongToBook { Name = "Автор" },
                    new TypeBelongToBook { Name = "Соавтор" }
                };

                // Добавление типов принадлежности к книге в контекст и сохранение изменений
                Context.TypeBelongToBooks.AddRange(typesBelong);
                await Context.SaveChangesAsync();
            }

            if (!Context.TypeConnections.Any())
            {
                // Инициализация типов связей
                var typesConnection = new List<TypeConnection>
                {
                    new TypeConnection { Name = "Партнер" },
                    new TypeConnection { Name = "Ребенок-родитель" },
                    new TypeConnection { Name = "Сиблинг" }
                };

                // Добавление типов связей в контекст и сохранение изменений
                Context.TypeConnections.AddRange(typesConnection);
                await Context.SaveChangesAsync();
            }

        }
    }
}
