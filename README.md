# Manifest Reader
**Read the manifest of the application on all platforms.**

Manifest Reader ona verdiğiniz uygulamanın ( APK, XAP, IPA ) manifest verilerini okumanızı sağlar. Bu sayede uygulamalarınızın Beta test & Production ve Enterprise süreçlerinde manifest bilgilerini okumada size kolaylık sağlar.

## Usage and Examples
### XAP Manifest Reader
XAP uygulamaların manifest bilgileri içinde bir çok tag yer almakta. Proje içinde kullanıcağınız tagleri kod kısmında düzenleyebilirsiniz. Ufak bir örnek verelim.

[![image](http://i.hizliresim.com/v54dlD.png)](http://hizliresim.com/v54dlD)

(31,32)(37,38) Satırlarda görüldüğü gibi tercihe göre yeni bir attribute eklenebilir.
Ayrıca ihtiyaca göre yeni bir class'ta tanımlayabilirsiniz.
```sh
public class IconPath
    {
        [XmlAttribute("IsRelative")]
        public Boolean IsRelative { get; set; }

        [XmlAttribute("IsResource")]
        public Boolean IsResource { get; set; }
    }
```
gibi kullanılabilinir. 

* Burada class'ların ve attribute'lerin ne manaya geldiğini anlamadıysanız WMAppManifest.xml dosyasını incelemenizi tavsiye ederim. 
* Ayrıca XmlElement ve XmlAttribute gibi kavramlara yabancı iseniz **System. Xml.Serialization** kütüphanesini incelemenizi öneririm.