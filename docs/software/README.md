# Реалізація інформаційного та програмного забезпечення
  
## SQL-Скрипт для створення початкового наповнення бази даних

```sql
<!-- @include: ./sql/db.sql -->
```

## RESTfull сервіс для роботи зі змінами

### Підключення до бази данних

<!-- @include: ./RESTApi/Db_conn.cs -->

### API контроллер

<!-- @include: ./RESTApi/updateController.cs -->

### Строка підключення

<!-- @include: ./RESTApi/appSettings.json -->

