<div align="center">
  <img src="ruta/a/tu/logo-hashbank.png" alt="Hash Bank Logo" width="150">
  <h1>Hash Bank</h1>
  <p>
    <strong>Billetera Virtual Segura con enfoque en Integridad de Datos y Prevención de Fraude.</strong>
  </p>

  <p>
    <img src="https://img.shields.io/badge/.NET-8.0-purple?style=flat-square&logo=dotnet" alt=".NET 8" />
    <img src="https://img.shields.io/badge/Angular-17+-red?style=flat-square&logo=angular" alt="Angular" />
    <img src="https://img.shields.io/badge/SQL%20Server-Database-c0c0c0?style=flat-square&logo=microsoft-sql-server" alt="SQL Server" />
    <img src="https://img.shields.io/badge/License-MIT-green?style=flat-square" alt="License" />
  </p>
</div>

---

## 📋 Sobre el Proyecto

**Hash Bank** no es solo una simulación de billetera virtual; es un ejercicio de ingeniería de software enfocado en la **Ciberseguridad** y la **Arquitectura Robusta**.

El objetivo principal de este proyecto es demostrar cómo construir un sistema financiero resistente a vulnerabilidades comunes, aplicando prácticas de **Programación Defensiva** y principios **SOLID**. A diferencia de un CRUD tradicional, Hash Bank prioriza la consistencia transaccional y la seguridad lógica.

### 🎯 Objetivos Técnicos
* **Prevención de Race Conditions:** Implementación de bloqueos y transacciones ACID para evitar doble gasto.
* **Inmutabilidad:** Arquitectura de "Ledger" donde las transacciones no se editan ni eliminan.
* **Seguridad Ofensiva/Defensiva:** Protección contra inyecciones SQL, XSS y manejo seguro de sesiones (JWT).
* **Calidad de Código:** Desarrollo guiado por pruebas (TDD) usando xUnit.

---

## 🚀 Stack Tecnológico

### Backend (Core)
* **Framework:** .NET 8 / 9 (Web API)
* **Lenguaje:** C#
* **ORM:** Entity Framework Core (Code-First)
* **Base de Datos:** SQL Server
* **Testing:** xUnit + Moq (Unit Testing & Integration Testing)
* **Seguridad:** JWT (JSON Web Tokens), BCrypt para hashing.

### Frontend (Cliente)
* **Framework:** Angular 17+
* **Estilos:** Bootstrap / CSS Custom
* **Arquitectura:** Componentes Standalone, Servicios tipados y Guards.

---

## 🏛️ Arquitectura de Base de Datos

El modelo de datos ha sido diseñado siguiendo la 3ra Forma Normal (3NF) para asegurar la integridad.

* **Users:** Gestión de identidad y credenciales (Hashed).
* **Accounts:** Soporte multi-moneda (ARS, USD) separada del usuario.
* **Transactions:** Registro histórico inmutable de movimientos.
* **Concepts:** Catálogo normalizado de motivos de transacción para evitar redundancia.

---

## 🛠️ Funcionalidades (Roadmap)

### Fase 1: Core Bancario (Backend) ✅ *En Progreso*
- [ ] Configuración de Clean Architecture (Domain, Infrastructure, API).
- [ ] Diseño de Entidades y Relaciones EF Core.
- [ ] Implementación de Unit of Work y Repository Pattern.
- [ ] **TDD:** Tests unitarios para lógica de transferencia y validación de saldos.

### Fase 2: Lógica de Negocio y Seguridad
- [ ] Sistema de Registro y Login con JWT.
- [ ] Lógica de Transacciones atómicas (evitar saldos negativos).
- [ ] Middleware de manejo de errores global.

### Fase 3: Interfaz de Usuario (Angular)
- [ ] Dashboard de saldos.
- [ ] Historial de movimientos.
- [ ] Formulario de transferencias con validaciones reactivas.

---

## 🔧 Instalación y Despliegue

*(Instrucciones pendientes de completar una vez inicializado el código fuente)*

1. Clonar el repositorio.
2. Configurar la cadena de conexión en `appsettings.json`.
3. Ejecutar `update-database` para aplicar migraciones.
4. Ejecutar `npm install` en la carpeta del cliente.

---

<div align="center">
  <sub>Desarrollado con fines educativos y de portafolio por [Tu Nombre].</sub>
</div>
