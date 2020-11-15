# Process and Translate Speech with Azure Cognitive Speech Services

## Transcribe speech input to text

### 1. Wstęp

Usługa Transcribe speech input to text pozwala na przetwarzanie mowy na tekst. 
Mowa może być zapisana w pliku audio lub strumieniowana bezpośrednio z mikrofonu, dzięki czemu uzyskujemy przetwarzanie w czasie rzeczywistym.
Możliwe jest dostosowanie modelu do indywidualnych potrzeb po dostarczeniu próbek głosu z etykietami.
Może to jednak powodować naliczanie dodatkowych opłat za hostowanie modelu.

### 2. Przypadki użycia

* Przetwarzanie komend głosowych i wykonywanie ich przez urządzenie (np. system inteligentnego domu)
* Stworzenie bota do wykorzystania na infolinii telefonicznej
* Umożliwienie obsługi aplikacji/urządzenia osobom niewidomym

### 3. Użytkowanie

Komunikacja z usługą może być realizowana z wykorzystaniem REST API lub biblioteki SDK. 
SDK jest dostępne dla 8  języków, m. in. C#, Java i Pyhon.

**Cennik**

Plan darmowy umożliwia przetworzenie 5 godzin audio (naliczanie sekundowe) miesięcznie bez opłat na standardowym modelu przetwarzania oraz kolejne 5 gdzin na modelu niestandardowym. Hostowanie jednego modelu w tym planie jest bezpłatne.
W planie standardowym opłaty wynoszą 1$ i 1,40$ za przetwarzanie godziny ścieżki dźwiękowej odpowiednio modelem standardowym i niestandardowym.
Ponad to hostowanie każdego niestandardowego modelu kosztuje 2,10$ za godzinę.

## Synthesize Text Input to Speech

### 1. Wstęp

Usługa Synthesize Text Input to Speech umożliwia syntezowanie mowy z dostarczonego tekstu. 
Dostępne jest 75 głosów mówiących w ponad 45 językach i dialektach, ale tylko dla wybranych dostępne są zaawansowane głosy neuronowe.
Głosy neuronowe brzmią bardziej naturalnie przez 

### 2. Przypadki użycia

* Dostosowanie urządzenia/aplikacji do użytku przez osoby niewidome i niedowidzące
* Umożliwienie odbierania informacji osobom bez pochłaniania ich pełnej uwagi- przydatne np. dla kierowców
* Wystawienie na infolinię telefoniczną bota, który do tej pory komunikował się tylko na kanałach tekstowych

### 3. Użytkowanie

**API**

Komunikacja odbywa się przez to samo SDK/API, z którego korzysta poprzednia usługa (Transcribe speech input to text)
Dostępne jest oddzielne API przeznaczone do przetwarzania długich ścieżek dźwiękowych (ponad 10 minut).
Zwraca ono plik audio zamiast strumienia, więc nie nadaje się do wykorzystania do przetwarzania w czasie rzeczywistym.

**Cennik**

W planie darmowym możliwe jest bezpłatne przetworzenie na mowę tekstów składających się z 5 milonów znaków (w sumie na miesiąc) dla głosów standardowych i 0,5mln znaków dla głosów neuronowych.
W planie standardowym opłaty wynoszą odpowiednnio 4$ i 16$ za przetworzenie miliona znaków głosem standardowym i neuronowym. 
Wyjątkiem jest tworzenie długich plkiów audio- za takie przetwarzenie głosem neuronowym płaci się 100$ za milion znaków.

## Translate speech with the speech service

### 1. Wstęp

Translate speech with the speech service to usługa służąca do natychmiastowej translacji mowy.
Danymi wejściowymi jest ścieżka dźwiękowa, a wyjściowymi tekst w postaci niezmienionej oraz po translacji.
W połączeniu z opisaną powyżej usługą zamiany tekstu na mowę możemy uzyskać efekt translacji mowy na mowę.
Możliwa jest jednoczesna translacja na wiele języków.

### 2. Przypadki użycia

* Realizacja ciągłej rozmowy głosowej między osobami posługującymi się różnymi językami
* Prowadzenie prezentacji udostępnianej jednocześnie w wielu językach
* Zastąpienie ludzkiego lektora

### 3. Użytkowanie

**API**

Z usługi można korzystać wykorzystując te same SDK, co we wcześniej opisanych usługach.
Translacja nie wymaga oddzielnego zapytania.
Tekst w języku docelowym jest zwracany razem z wynikiem zamiany mowy na tekst.

**Cennik**

W planie darmowym można bezpłatnie przeprowadzić translację 5 godzin ścieżki dźwiękowej miesięcznie.
W planie standardowym opłata za translację godziny audio wynosi 2,50$