# WebAPI
## Тема: веб приложение для создания и развития персонажей. 
### Многоуровневая архитектура

Создана в соотвествии с данным руководством на metanit.com: https://metanit.com/sharp/mvc5/23.5.php

(За исключением слоя DAL из руководства)

![image](https://github.com/user-attachments/assets/4476f9c9-10cd-4b9e-90c6-6bcee2931fb6)


### БД
БД создана с использованием Entity, MSSql. 

ER-диаграмма на это дело:

![image](https://github.com/user-attachments/assets/62f8f3ed-e197-46fd-9c25-2e0beffa33e5)


Справочники:

![image](https://github.com/user-attachments/assets/1e7092f6-7415-481c-86c3-e7be74c10537)


### Бэк

Бэк: получение запросов от клиента на сохранение этих данных, выведение, изменение, удаление. 

Диаграмма прецендентов на это дело:

![Копия Диаграммки-Диаграмма прецедентов вместе](https://github.com/user-attachments/assets/8cd8c05d-1a37-4238-aabc-949fdb367edd)

### AutoMapper

AutoMapper выполнен по этой статье: https://dev.to/sardarmudassaralikhan/automapper-in-aspnet-core-7-web-api-jce

И по этой статье: https://metanit.com/sharp/mvc5/23.4.php?ysclid=lz5dui5i7p894485598

### Authorize

Через JWT-токен.

### Ошибки

Нормализованы через мидлвейр. В основном по этой статье: https://metanit.com/sharp/aspnet6/2.16.php?ysclid=m0ck8x0ceu613011550

### Тесты
Тесты (в папке Test): файлы с http запросами. Для каждого контроллера свой.
