import numpy as np
import cv2
from azure.cognitiveservices.vision.customvision.training import CustomVisionTrainingClient
from azure.cognitiveservices.vision.customvision.prediction import CustomVisionPredictionClient
from azure.cognitiveservices.vision.customvision.training.models import ImageFileCreateBatch, ImageFileCreateEntry, Region
from msrest.authentication import ApiKeyCredentials
import time
import skimage.io as io

# Replace with valid values
ENDPOINT = "https://northeurope.api.cognitive.microsoft.com/"
# training_key = "<your training key>"
prediction_key = "21d39651b32949968425de2663aa9581"
prediction_resource_id = "/subscriptions/8e5b16fd-b1ed-4200-8944-db3b194c548c/resourceGroups/AzureMLComputerVision/providers/Microsoft.CognitiveServices/accounts/AzureMlComputerVisionCustomVision"
project_id = "83dcfe6f-e860-4af3-806e-8b0b0e23eca5"
publish_iteration_name = "NoMaskDetector_i4"

no_mask_threshold = 0.5
with_mask_threshold = 0.5
font = cv2.FONT_HERSHEY_SIMPLEX
fontScale = 1
fontColorNeutral = (255,255,255)
fontColorGood = (0,255,0)
fontColorBad = (0,0,255)
bbox_thickness = 2
lineType = 2

prediction_credentials = ApiKeyCredentials(in_headers={"Prediction-key": prediction_key})
predictor = CustomVisionPredictionClient(ENDPOINT, prediction_credentials)

wideo = cv2.VideoCapture(1)

if (wideo.isOpened()):
    while(True):
        udany_odczyt, ramka = wideo.read()
        if udany_odczyt:
            k = cv2.waitKey(25) & 0xFF
            if k == 27 :
                break
            
            img = cv2.imwrite("temp.jpg", ramka)
            with open("temp.jpg", "rb") as image_contents:
                results = predictor.detect_image(
                    project_id, publish_iteration_name, image_contents.read())
            
                valid_predictions = [pred for pred in results.predictions 
                                     if (pred.tag_name == "with_mask" and pred.probability > with_mask_threshold)
                                         or (pred.tag_name == "no_mask" and pred.probability > no_mask_threshold)]
                # Display the results.
                for prediction in valid_predictions:
                    print("\t" + prediction.tag_name +
                          ": {0:.2f}%".format(prediction.probability * 100))
                    lx = int(prediction.bounding_box.left * ramka.shape[1]) 
                    rx = int((prediction.bounding_box.left + prediction.bounding_box.width) * ramka.shape[1]) 
                    by = int((prediction.bounding_box.top + prediction.bounding_box.height) * ramka.shape[0])
                    uy = int(prediction.bounding_box.top * ramka.shape[0])

                    if prediction.tag_name == "with_mask" :
                        pred_color = fontColorGood 
                        text = "Thank You!"
                    else :
                        pred_color= fontColorBad
                        text = "Wear mask!"
                    
                    #lline
                    cv2.line(ramka, (lx,uy), (lx,by), pred_color, bbox_thickness)
                    #rline
                    cv2.line(ramka, (rx,uy), (rx,by), pred_color, bbox_thickness)
                    #bline
                    cv2.line(ramka, (lx,by), (rx,by), pred_color, bbox_thickness)
                    #uline
                    cv2.line(ramka, (lx,uy), (rx,uy), pred_color, bbox_thickness)
                    
                    x_text = lx + bbox_thickness + 1
                    y_text = by - bbox_thickness - 1
                    cv2.putText(ramka, text, (x_text,y_text), font,fontScale,pred_color,lineType)

                if len(valid_predictions) == 0:
                    print("No face detected")
                    cv2.putText(ramka, "No face detected", (10,20), font,fontScale,fontColorNeutral,lineType)
                    
                
                
            cv2.imshow('ramka',ramka)


            
            

    wideo.release()
    cv2.destroyAllWindows()



             