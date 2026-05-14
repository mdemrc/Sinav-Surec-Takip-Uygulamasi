# Exam Process Tracker (YKS-focused) (`Sinav-Surec-Takip-Uygulamasi`)

[![SQL Server](https://img.shields.io/badge/SQL-SQL%20Server-CC2927?logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)
[![WinForms](https://img.shields.io/badge/UI-WinForms-blue)](https://learn.microsoft.com/dotnet/desktop/winforms/)

## English

### Overview

Long-horizon **exam preparation cockpit** consolidating **courses, topics, study resources**, and optional mock-exam artefacts into one SQL-backed workflow (`SınavSurecTakip` database). Designers rely heavily on grids and dialogs for disciplined CRUD, echoing planners who annotate why a topic reopened after regression.

Connection strings utilise **`Integrated Security=True`** targeting **`(local)\SQLEXPRESS`**; align with departmental SQL provisioning policies.

---

## Türkçe

### Genel bakış

**Sınav süreç takip** uygulaması uzun süreli **ders / konu / kaynak / deneme** yaşam döngüsünün tek veritabanında \(\`SınavSurecTakip\`\) tutulduğu WinForms aracıdır. Data grid odaklı ekranlarla haftalık tekrar disiplinine görsel bağlanır.

### Bağlantı notu

`App.config`, `SqlBaglantisi.cs` ve `Settings.*` tasarımcı çıktılarının hepsi uyumlu `Data Source` içermeli; akademik olarak `(local)\SQLEXPRESS` varsayılmıştır.
