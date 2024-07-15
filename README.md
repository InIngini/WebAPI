# WebAPI
## Тема: веб приложение для создания и развития персонажей. 
### Многоуровневая архитектура

Создана в соотвествии с данным руководством на metanit.com: https://metanit.com/sharp/mvc5/23.5.php

![Копия Диаграммки-Страница — 5 (1)](https://github.com/user-attachments/assets/b1e9d50e-a591-4b56-83ed-7a6aaf125343)

### БД
БД создана с использованием Entity, MSSql. 

ER-диаграмма на это дело:

![схема (1)](https://github.com/user-attachments/assets/d313b31a-13d6-4b73-b3e4-7f9215091c33)

Справочники:

![oF2xiQzUoTA](https://github.com/user-attachments/assets/d4803351-b48b-4914-baa4-260c3484505b)

### Бэк

Бэк: получение запросов от клиента на сохранение этих данных, выведение, изменение, удаление. 

Диаграмма прецендентов на это дело:

![Frame 6](https://github.com/InIngini/WebAPI/assets/130362544/9ae92e41-1b39-444d-92b3-696e2eb73a16)

### Тесты
Тесты (в папке Test): файлы с http запросами. Для каждого контроллера свой.
