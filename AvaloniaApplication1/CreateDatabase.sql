create table roles
(
    id   serial primary key,
    name varchar(50) not null
);

create table users
(
    id           serial primary key,
    name         varchar(150) not null,
    surname      varchar(150) not null,
    phone_number varchar(11)  not null,
    birthdate    text         not null,
    login        varchar      not null,
    password     varchar      not null,
    id_role      int references roles (id)
);
