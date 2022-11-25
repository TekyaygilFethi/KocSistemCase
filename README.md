
# Koç Üniversitesi Öğrenci Sistemi

Koç Üniversitesi Öğrenci Sistemi öğrencilerin listelenmesi, eklenmesi ve değiştirilmesi temelli çalışır. Yapı öğrenciler ve aldıkları derslerden oluşur. Aşağıdaki rolleri ve işlemler desteklenir:

- Admin: 
    - Öğrenci Ekleme
    - Öğrenci Güncelleme (Tüm öğrenciler)
    - Öğrenci Görüntüleme (Tüm öğrencilerin dersleri dahil)

- User:
    - Öğrenci Görüntüleme (Tüm öğrencilerin bilgilerini görebilir ancak yalnızca kendi derslerini görebilir)
    - Öğrenci Güncelleme (Yalnızca kendi kaydını güncelleyebilir)

# Kullanılan Teknolojiler

- **.NET 6 MVC:** Ana proje Framework'ü
- **Serilog & Elastic Search:** Logların saklanması ve filtrelenmesi
- **Entity Framework Code First:** Veritabanı nesnelerinin oluşturulması ve CRUD işlemlerinin gerçekleştirilmesi
- **MS SQL**: Veritabanı
- **Redis**: Cachelerin tutulması

# API ve Web Referansları
Projede 2 tür controller vardır. Bunlardan biri API Controller, diğeri ise MVC Controller'dır.

MVC Controller view dönerken, API Controllerlar data döndürür.
### Global Hata Handler Middleware
Projede bir hata olma durumunda hata, GlobalErrorHandlerMiddleware tarafından handle edilir. Bu middleware hatayı elasticsearch'e yazar ve error sayfasına yönlendirme yapar.

API Controller'dan çağrılan bazı hatalar bu middleware'e girmek yerine sayfada bir geri dönüt verebilmek için custom handle edilir.

### ResponseObject
Bu obje API Controller'ın çağırdığı servislerden dönen cevaptır.

```csharp
public class ResponseObject<T>
{
    public bool IsSuccess { get; set; } = true;
    public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
    public string Message { get; set; } = String.Empty;
    public T Data { get; set; }
}
```

## Lokalde Çalıştırma

Projeyi klonlayın

```bash
  $ git clone https://github.com/TekyaygilFethi/KUSYS.git
```

Projenin ana dizini olan ```./KUSYS``` klasörüne gidin. 
```bash
  $ cd ./KUSYS
```

Eğer lokalinizde çalışan bir ElasticSearch ve Kibana varsa bu kısmı atlayabilirsiniz. Redis farklı bir sunucuda çalışıyorsa ```KUSYS\KUSYS.WEBUI\appsettings.json``` dosyası altındaki ```ElasticConfiguration``` altındaki ```Uri``` değerini değiştirmelisiniz. ElasticSearch ve Kibana uygulamalarını Docker'da ayağa kaldırmak için aşağıdaki komutu çalıştırın:
```bash
  $ docker-compose up -d
```

Eğer lokalinizde çalışan bir redis varsa bu kısmı atlayabilirsiniz. Redis farklı bir sunucuda çalışıyorsa ```KUSYS\KUSYS.WEBUI\appsettings.json``` dosyası altındaki ```RedisConfiguration``` altındaki ```Uri``` değerini değiştirmelisiniz. Redis sunucunuz yoksa ve Docker'ınız kuruluysa aşağıdaki komutu çalıştırarak Redis'i Docker yardımıyla ayağa kaldırabilirsiniz:
```bash
  $ docker run --name my-redis -p 6379:6379 -d redis
```

Porjeyi çalıştırın:

```bash
  $ dotnet run
```

## Docker ile Çalıştırma

Aşağıdaki komutu çalıştırarak Dockerfile yardımıyla projeyi Docker'da build edin:
```bash
  $ docker build --rm -t  fethitekyaygil-dev/kusys:latest .
```

Build işlemi bittikten sonra aşağıdaki komutla Docker image çalıştırılabilir:

```bash
  $ docker run --rm --name kusys -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 ASPNETCORE_URLS=http://+:5000 fethitekyaygil-dev/kusys
```

Sonrasında siteye aşağıdaki linkten erişim sağlayabiliyor olacaksınız:


```
http://localhost:5000/
```

## Geliştiriciler

- [@TekyaygilFethi](https://www.github.com/TekyaygilFethi)


## Demo

Site ilk açıldığında sağ üstteki kısımdan login olabilirsiniz. Deneme için oluşturulan kullanıcılar aşağıdaki gibidir:

### Credentials

| Username  | Password  | Role  |
|:----------|:----------|:--------  |
| fethitekyaygil    | 1q2w3e4r    | Admin  |
| yaseminozen    | 123456    | User  |
| tahatekyaygil    | 987654    | User  |



Giriş işleminden sonra yönlendirileceğiniz ana sayfada yapabileceğiniz tüm işlem seçenekleri belirtilmiştir.
