# AR Balık Tutma Oyunu - Teknik Dokümantasyon

## İçindekiler
1. [Proje Genel Bakış](#proje-genel-bakış)
2. [Teknik Gereksinimler](#teknik-gereksinimler)
3. [Proje Yapısı](#proje-yapısı)
4. [AR Sistemi](#ar-sistemi)
5. [Su Fizikleri](#su-fizikleri)
6. [Balık AI](#balık-ai)
7. [Kullanıcı Arayüzü](#kullanıcı-arayüzü)
8. [Performans Optimizasyonu](#performans-optimizasyonu)
9. [Derleme ve APK Oluşturma](#derleme-ve-apk-oluşturma)
10. [Bilinen Sorunlar ve Çözümleri](#bilinen-sorunlar-ve-çözümleri)

## Proje Genel Bakış

AR Balık Tutma Oyunu, Unity ve AR Foundation kullanılarak geliştirilen artırılmış gerçeklik tabanlı bir mobil uygulamadır. Proje, çocukların eğlenceli bir şekilde balık tutma aktivitesi yapabilmelerini sağlar. Bu doküman, projenin teknik detaylarını içerir ve geliştiricilere rehberlik etmeyi amaçlar.

## Teknik Gereksinimler

### Geliştirme Ortamı
- **Unity Sürümü**: 2022.3 LTS veya daha yüksek
- **AR Foundation**: 5.0 veya daha yüksek
- **ARCore**: 1.35.0 veya daha yüksek
- **IDE**: Visual Studio 2019/2022 veya Visual Studio Code
- **Minimum SDK Sürümü**: Android API Level 24 (Android 7.0)
- **Hedef SDK Sürümü**: Android API Level 33 (Android 13)

### Paket Bağımlılıkları
Aşağıdaki Unity paketleri projede kullanılmaktadır:
- AR Foundation
- ARCore XR Plugin
- XR Plugin Management
- Universal Render Pipeline (URP)
- TextMeshPro

## Proje Yapısı

Proje, aşağıdaki klasör yapısına sahiptir:

```
Assets/
├── Prefabs/              # Oyunda kullanılan hazır nesneler
├── Scenes/               # Oyun sahneleri
├── Scripts/              # C# script dosyaları
├── Models/               # 3D modeller (balıklar, olta, vb.)
├── Materials/            # Materyal dosyaları
├── Textures/             # Doku dosyaları
├── Animations/           # Animasyon dosyaları
├── Fonts/                # Font dosyaları
├── Sounds/               # Ses dosyaları
├── Plugins/              # Eklentiler
└── XR/                   # XR/AR ile ilgili dosyalar
```

### Anahtar Dosyalar ve Açıklamaları

- `Assets/Scenes/FirstScene.unity`: Başlangıç ekranı
- `Assets/Scenes/DetectScene.unity`: AR algılama ve oyun sahnesi
- `Assets/Scripts/ARFilterPlanes.cs`: AR düzlem algılama ve filtreleme
- `Assets/Scripts/ARFeatheredPlaneMeshVisualizer.cs`: AR düzlem görselleştirme
- `Assets/Scripts/CreateWaterMesh.cs`: Su yüzeyi oluşturma

## AR Sistemi

Oyun, AR Foundation ve ARCore kullanarak gerçek dünya yüzeylerini algılar ve bir AR deneyimi sunar.

### Yüzey Algılama

Uygulama, AR Foundation'ın plane detection (düzlem algılama) özelliğini kullanarak düz yüzeyleri algılar. Bu işlem `ARFilterPlanes.cs` script'i tarafından yönetilir:

```csharp
// ARFilterPlanes.cs içinden
private void OnPlanesChanged(ARPlanesChangedEventArgs args) {
    if(arPlane==null && args.added !=null && args.added.Count > 0 ) {
        arPlane = args.added[0];
    }

    foreach(ARPlane plane in args.added) {
        plane.gameObject.SetActive(false);
    }

    arPlane.gameObject.SetActive(true);
}
```

### Düzlem Görselleştirme

Algılanan düzlemler, `ARFeatheredPlaneMeshVisualizer.cs` script'i tarafından görselleştirilir. Bu script, düzlemlerin kenarlarını yumuşatarak daha estetik bir görünüm sağlar.

## Su Fizikleri

Oyunda gerçekçi bir su ortamı yaratmak için özel bir mesh sistemi kullanılmıştır.

### Su Mesh Oluşturma

Su yüzeyi, `CreateWaterMesh.cs` script'i tarafından dinamik olarak oluşturulur. Bu script, algılanan düzlemin boyutlarına göre bir su mesh'i oluşturur:

```csharp
// CreateWaterMesh.cs içinden örnek kod
public void CreatePlane(float width, float height, Vector3 center) {
    // Mesh oluşturma kodu
    Mesh mesh = new Mesh();
    
    // Vertex pozisyonları
    // UV koordinatları
    // Normaller
    // Üçgenler
    
    // Mesh'i atama
    meshFilter.mesh = mesh;
    
    // Su davranışlarını ayarlama
}
```

### Su Davranışları

Su yüzeyi, shader kullanılarak dalga efektleri ve ışık yansımaları ile gerçekçi bir görünüm elde eder. Su fizikleri, balıkların hareketine ve olta etkileşimlerine tepki verir.

## Balık AI

Balıklar, basit bir yapay zeka sistemi ile kontrol edilir.

### Balık Davranışları

Balıkların sürü davranışı, kaçma mekanizmaları ve oltaya yaklaşma özellikleri vardır. Bu davranışlar aşağıdaki parametrelerle kontrol edilir:

- Hareket hızı
- Dönüş oranı
- Fark etme mesafesi
- Sürü davranışı kuralları

## Kullanıcı Arayüzü

Oyun içi UI, Unity'nin Canvas sistemi ve TextMeshPro kullanılarak oluşturulmuştur.

### UI Bileşenleri

- Açılış ekranı
- AR algılama rehberi
- Oyun içi kontroller (olta kontrolleri, çekme butonu)
- Puan göstergeleri
- Ayarlar menüsü

## Performans Optimizasyonu

Mobil cihazlarda iyi performans elde etmek için aşağıdaki optimizasyonlar uygulanmıştır:

- Düşük poly modeller
- Atlas texture kullanımı
- Mesh batching
- LOD (Level of Detail) sistemi
- Object pooling

## Derleme ve APK Oluşturma

Projeyi derlemek ve APK oluşturmak için aşağıdaki adımları izleyin:

1. Unity Editor'de, **File > Build Settings**'i açın
2. Platform olarak Android'i seçin ve **Switch Platform** butonuna tıklayın
3. **Player Settings**'i açın ve şunları ayarlayın:
   - **Company Name**: Ekibinizin Adı
   - **Product Name**: AR Balık Tutma
   - **Version**: 1.0
   - **Bundle Identifier**: com.ekibinizinadi.arbaliktutma
   - **Minimum API Level**: Android 7.0 'Nougat' (API level 24)
   - **Target API Level**: Android 13 (API level 33)
4. **XR Plugin Management** ayarlarında ARCore'un etkinleştirildiğinden emin olun
5. **Build** butonuna tıklayın ve APK dosyasını kaydedin

## Bilinen Sorunlar ve Çözümleri

### AR Performans Sorunları
- **Sorun**: Düşük ışık koşullarında yüzey algılama sorunları
- **Çözüm**: Aydınlık ortamlarda kullanımı önerin ve kamera ayarlarını optimize edin

### Bellek Yönetimi
- **Sorun**: Uzun süre kullanımda bellek sızıntısı
- **Çözüm**: Object pooling sistemini geliştirin ve gereksiz nesneleri temizleyin

### Cihaz Uyumluluğu
- **Sorun**: Bazı eski cihazlarda AR desteği sorunu
- **Çözüm**: ARCore uyumluluk kontrolünü geliştirin ve uyumsuz cihazlarda kullanıcıya bilgi verin 