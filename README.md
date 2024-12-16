# Proje Kurulumu ve Test Talimatları

Bu doküman, projenin nasıl kurulacağı ve test edileceği ile ilgili adımları detaylı bir şekilde açıklamaktadır. Lütfen aşağıdaki adımları takip edin.

---

## Proje Kurulumu

1. **Proje Dosyalarını Açın:**
   - Proje dosyalarını bilgisayarınıza indirin ve bir geliştirme ortamında açın (örneğin, Visual Studio veya Visual Studio Code).

2. **Connection String Ayarları:**
   - Projedeki `appsettings.json` veya ilgili konfigürasyon dosyasını açın.
   - `ConnectionString` kısmını kendi MSSQL bağlantı bilgileriniz ile değiştirin. Örnek bir bağlantı stringi:
     ```json
     "ConnectionStrings": {
     "MsSQLConnection": "Server=YourServerName;Database=Your_DbName;Integrated Security=True;TrustServerCertificate=True;"
      }
     ```

3. **Migrations Uygulama:**
   - Proje dosyalarını açtıktan sonra terminalde şu komutları çalıştırarak veritabanı migrasyonlarını uygulayın:
     ```bash
     dotnet ef database update
     ```

---

## Kullanıcı Bilgileri

- **Admin Hesabı:**
  - **E-posta:** admin@inveon.com
  - **Şifre:** InveonAdmin1!

---

## Test Adımları

### 1. Admin Girişi
   - Uygulamayı başlattıktan sonra giriş ekranına gidin.
   - Yukarıda belirtilen admin hesabını kullanarak giriş yapın.

### 2. Kullanıcı Ekleme ve Rol Yönetimi
   - **Kullanıcı Ekleme:**
     - Yönetim panelinde "Kullanıcı Ekle" bölümüne gidin.
     - Yeni bir kullanıcı eklemek için gerekli bilgileri doldurun ve kaydedin.
   - **Rol Yönetimi:**
     - Kullanıcıların rollerini düzenlemek için ilgili kullanıcıyı seçin.
     - Rollerini atayın veya değiştirin ve kaydedin.

### 3. Kitap Ekleme
   - **Kitap Ekleme:**
     - "Book" sayfasına gidin.
     - Yeni bir kitap eklemek için butona tıklayın.

### 4. Kitap Detaylarını Görüntüleme
   - "Book" sayfasında eklediğiniz kitabın üzerine tıklayın.
   - Kitaba ait detay sayfası açılacaktır. Kitap bilgilerini kontrol edin.

---


