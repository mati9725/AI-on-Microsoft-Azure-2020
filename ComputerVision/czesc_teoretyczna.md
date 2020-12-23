## Computer Vision API

### 1.Wstęp

#### Face API

* Wykrywanie i rozpoznawanie twarzy (grupowanie twarzy tej samej osoby)
* Lokalizowanie punktów kluczowych twarzy
* Rozpoznawanie atrybutów takich jak wiek, płeć lub emocje

#### Emotion API

Wykrywanie emocji (np. złość, strach, szczęście, zaskoczenie)

### 2.Przypadki użycia

* Dobór treści dla osoby siedzącej przed koputerem na podstawie jej nastroju
* Rozpoznawanie twarzy osób poszukiwanych w tłumie
* Wykrywanie osób z zakazem stadionowym podczas meczy

### 3.Sposób użycia

Do korzystania z usługi można wykorzystać REST API lub SDK dostępne np. dla C# i Python

**Opłaty**

- W planie darmowym dostępne jest 5000 transakcji miesięcznie przy ograniczeniu do 20 transakcji na minutę
- W planie standardowym (S1) maksymalna częstotliwość to 10 transakcji na sekundę. Cena zależy od tego z jakiej funkcji API korzystamy i jak transakcji miesięcznie wykonujemy- im więcej transakcji, tym niższe opłaty za 1000 transakcji.

## Custom Vision

### 1.Wstęp

Usługa Custom Vision pozwala na proste i szybkie tworzenie klasyfikatorów. 
Uczenie modelu polega na dostarczeniu mu obrazów i określeniu klas, do których powinny one być przyporządkowane.

### 2.Przypadki użycia

* Wykrywanie osób nie noszących maseczek w miejscach publicznych
* Rozpoznawanie gatunku kwiatów na podstawie zdjęcia
* Rozpoznawanie ras psów na obrazach

### 3.Sposób użycia

Model trenuje się wykorzystując GUI portalu Azure.
Najpierw wgrywa się zdjęcia, a następnie opisuje je tagami oznaczającymi klasę.
Pouruchomieniu i zakończeniu treningu system wylicza wskaźniki jakości takie jak czułość i precyzja osiągane na danych treningowych.
Następnie można przetestować wyszkolony model ładując dane, które nie były wykorzystywane do treningu.
Następnie model może zostać opublikowany i można go wywoływać przez REST API.

**Opłaty**
* W planie darmowym użytkownik może posiadać maksymalnie 2 projekty, a łączny czas szkolenia modeli nie może przekroczyć godziny miesięcznie. Kolejnymi ograniczniami jest limit transakcji, do 10tys. miesięcznie nie częściej niż 2 razy na sekundę, oraz limit załadowanych zdjęć treningowych wynoszący 5tys zdjęć w jednym projekcie.
* W planie standardowym model może być wywoływany do 10 razy na sekundę. Użytkownik może posiadać nawet 100 projektów. Płatności rozliczane są na podstawie ilości transakcji, czasu szkolenia modeli oraz liczby składowanych zdjęć treningowych.


## Video Indexer

### 1.Wstęp

Usługa Video Indexer służy do indeksowania i wyszukiwania treści w plikach wideo. 
Model może być dotrenowywany i dostosowywany do indywidualnych potrzeb.
Usługa ma m.in. takie funkcje jak rozpoznawanie twarzy, tekstu i obiektów widocznych na filmach.

### 2.Przypadki użycia

* Rozpoznawanie twarzy poszukiwanych osób na ulicach
* Analiza obrazu nagrywanego przez kamery monitoringu
* Liczenie pojazdów w celu dynamicznego dostosowania czasu między zmianami świateł


### 3.Sposób użycia

Z usługi można korzystać z użyciem REST API . 
Film można zuploadować do usługi przez HTTP lub  najpierw umieścić go  w miejscu publicznie dostępnym, a do API wysłać tylko URL do niego.

**Opłaty**

Użytkownik ma możliwość nieodpłatnego przetworzenia 10 godzin filmu jeśli korzysta ze strony internetowej usługi, a jeśli używa API to nawet 40 godzin bez opłat.
Wysokość opłat zależy od jakości przetwarzanych filmów (np. rozdzielczości i stosowanych kodeków)
Warto zauważyć, że korzystając z tej usługi Azure może naliczać opłaty za usługi powiązane takie jak streaming czy storage.


