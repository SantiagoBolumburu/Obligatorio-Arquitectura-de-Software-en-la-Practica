# Obligatorio-Arquitectura-de-Software-en-la-Practica
El proyecto que hice para el obligatorio requerido en la materia "Arquitectura de Software" en la practica 

![Diagrama](https://github.com/SantiagoBolumburu/Obligatorio-Arquitectura-de-Software-en-la-Practica/assets/80825160/25dc993f-49a1-4878-9181-78fdbc2a20a5)

## Descripción general:
Este proyecto se divide en 6 partes: 1 frontend creado en Angular, y 5 microservicios que conforman el backend (4 escritos en C#, y uno en Gol (Golang)). Los 6 servicios originalmente se encontraban en un repositorio distinto cada uno.

El proyecto es un sistema de gestión de inventario, que permite a usuarios crearse cuentas y empresas, a las cuales pueden invitar a otros usuarios con distintos roles. Dichos usuarios pueden ingresar y eliminar nuevos productos y proveedores de estos, así como modificar su información. También se permite ingresar compras y ventas de productos: estas modifican el stock de los productos, y en el caso de las compras tienen un proveedor asociado. Además, se pueden generar reportes, los cuales son enviados por email, y subscribirse a productos para recibir (por email) actualizaciones de su stock y eventos (ventas y compras).

La decisión de que cada microservicio tenga su propio repositorio, se basa en el principio **I. Codebase** encontrado en **the 12 factor app**, que establece que cada aplicación (en este caso microservicio) debe tener si propia *codebase*, separada del resto. “[The 12 factor app](https://12factor.net/)” es una lista de principios redactados por desarrolladores de [Heroku](https://www.heroku.com/), en los que detallan características que una aplicación debería cumplir.

## Descripción de Arquitectura:
### Frontend:
El frontend es una SPA (single page application) echa en Angular, la cual se comunica mediante llamadas HTTP con el backend, más específicamente el servicio “API-Gateway”. Como se puede ver en el diagrama, se desplego en un “bucket” del servicio S3 de AWS.
### Backend:
El Backend es una aplicación dividida en 5 microservicios, todos hechos con para desplegarse en containers de Docker, y de los cuales 3 utilizan bases relacionales SQL (PostgreSQL). Para comunicarse entre sí, estos servicios implementan APIs REST HTTP. Estos servicios se desplegaron cada uno en instancias del servicio Elastic Beanstalk de AWS, y para las bases relacionales se emplearon instancias del servicio RDS de AWS.
