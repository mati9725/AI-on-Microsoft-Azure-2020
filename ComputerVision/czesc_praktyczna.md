# System do wykrywania czy ludzie mają maseczki

## Scenariusz

System ma za zadanie zaznaczać na obrazie twarze ludzi. 
Twarze w maseczkach mają być zaznaczane na zielono i ma się przy nich wyświetlać pozytywny tekst (np. "Dziękujemy")
Twarze bez maseczek mają być zaznaczane na czerwono i widoczny przy nich ma być napis "Załóż maseczkę"

Przykładem wykorzystania mogą być ekrany nad drzwiami wejściowymi do marketów. 
Na takim ekranie wchodzący widzi samego siebie, więc zobaczy też komunikat.
Takie zastosowanie ma na celu wpłynięcie  na klientów, aby zakładali oni maseczki.

## Wykonanie

Do realizacji rozwiązania wykorzystano usługę Custom Vision.
Do wytrenowania detektora wykorzytano zdjęcia pochodzące  z dwóch zbiorów:
https://www.kaggle.com/andrewmvd/face-mask-detection
http://vis-www.cs.umass.edu/fddb/
Pierwszy zbiór zawiera głównie zdjęcia ludzi w maseczkach. Aby ulepszyć wykrywanie twarzy bez maseczek wykorzystano też zdjęcia z drugiego zbioru.

Do szkolenia modelu wykorzystano 127 zdjęć.
Ramki na danych treningowych były dodawane manualnie, ponieważ funkcja wspomagania etykietowania aktualnie nie działa (po kliknięciu odpowidniego przycisku wyświetla się komunikat "Something went wrong", a wysyłane w tym czasie zapytanie AJAX dostaje w odpowiedzi status HTTP500).
Po dodaniu danych przeprowadzono szkolenie i opublikowano model. 

Następnym krokiem była implementacja aplikacji-klienta wykorzystującego opublikowany model.
Stworzona aplikacja przetwarza w czasie rzeczywistym obraz z kamery, zaznacza twarze i wyświetla przetworzony obraz.
Do wywoływania modelu wykorzystano pakiet azure.cognitiveservices.vision.customvision do języka Python. 

Demo:
https://youtu.be/ABr-91AGPLY