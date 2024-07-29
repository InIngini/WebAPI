# WebAPI
## Тема: веб приложение для создания и развития персонажей. 
### Многоуровневая архитектура

Создана в соотвествии с данным руководством на metanit.com: https://metanit.com/sharp/mvc5/23.5.php

![Копия Диаграммки-Архитектура](https://github.com/user-attachments/assets/bad7716a-af0e-4075-a2c2-389c170d9651)

### БД
БД создана с использованием Entity, MSSql. 

ER-диаграмма на это дело:

![схема (1)](https://github.com/user-attachments/assets/d313b31a-13d6-4b73-b3e4-7f9215091c33)

Справочники:

![oF2xiQzUoTA](https://github.com/user-attachments/assets/d4803351-b48b-4914-baa4-260c3484505b)

### Бэк

Бэк: получение запросов от клиента на сохранение этих данных, выведение, изменение, удаление. 

Диаграмма прецендентов на это дело:

![Копия Диаграммки-Диаграмма прецедентов вместе](https://github.com/user-attachments/assets/8cd8c05d-1a37-4238-aabc-949fdb367edd)

### AutoMapper

AutoMapper выполнен по этому видео: https://www.youtube.com/watch?v=Ssh3cBPjd4Y&list=PLEtg-LdqEKXbpq4RtUp1hxZ6ByGjnvQs4&index=5

И по этой статье: https://metanit.com/sharp/mvc5/23.4.php?ysclid=lz5dui5i7p894485598

### Authorize

Через JWT-токен.

### Тесты
Тесты (в папке Test): файлы с http запросами. Для каждого контроллера свой.
