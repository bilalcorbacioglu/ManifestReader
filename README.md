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

### APK Manifest Reader

XAP'ta olduğu gibi bu kısımda da işinize yarayan kısımları kullanabilirsiniz.

[![image](https://github.com/bilalcorbacioglu/ManifestReader/raw/master/image/1a.png)](https://github.com/bilalcorbacioglu/ManifestReader/raw/master/image/1a.png)

### IPA Manifest Reader
Öncelikle programı başlatmadan önce dikkat etmeniz gereken bir kaç nokta var.
[![image](https://github.com/bilalcorbacioglu/ManifestReader/raw/master/image/1i.png)](https://github.com/bilalcorbacioglu/ManifestReader/raw/master/image/1i.png)
[![image](https://github.com/bilalcorbacioglu/ManifestReader/raw/master/image/2i.png)](https://github.com/bilalcorbacioglu/ManifestReader/raw/master/image/2i.png)
[![image](https://github.com/bilalcorbacioglu/ManifestReader/raw/master/image/3i.png)](https://github.com/bilalcorbacioglu/ManifestReader/raw/master/image/3i.png)

Yukarıdaki resimlerde gördüğünüz gibi bir .ipa dosyasını açtığımızda, Payload adında default olarak bir klasör gelmekte. Payload içindeki dosyanın ismi ise "blabla.app" değişken olabilmektedir. Bu sebepten kodda ki şu kısımda sizden bu bilgiler istenmektedir.

[![image](https://github.com/bilalcorbacioglu/ManifestReader/raw/master/image/01i.png)](https://github.com/bilalcorbacioglu/ManifestReader/raw/master/image/01i.png)
PList'i direk zip dışına çıkarırsak, bozulmuş bir biçimde çıkacağını gözlemleyebilirsiniz. Bunun olmaması için 54-60 arasındaki satırları kullanarak extract ediyoruz.

[![image](https://github.com/bilalcorbacioglu/ManifestReader/raw/master/image/02i.png)](https://github.com/bilalcorbacioglu/ManifestReader/raw/master/image/02i.png)

64.Satırda ise düzgün bir biçimde çıkarmayı başarabildiğimiz Plist'i parse ediyoruz. Bize lazım olanları çekiyoruz. Eğer size başka özelliklerde lazımsa debug ederken parsedPlist'in içeriğine bakabilirsiniz.

Plist'in diğer manifest dosyalarına nazaran başka bir dez avantajı olduğu için parse etme ihtiyacı duyuluyor, sadece xml kütüphanesi işe yaramıyor.
65-69 Satırları arasında aşağıdaki xml syntax'ını parse etmek durumunda kalıyoruz.

```sh
<dict>
    <key>    .....     </key>
    <string> .....     </string>
    
    <dict>
        <key>    .....     </key>
        ....
    </dict>

</dict>
```