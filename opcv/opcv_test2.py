import cv2

import numpy as np

img = cv2.imread("./image/ieu-01.jpg")
gray = cv2.cvtColor(img , cv2.COLOR_BGR2GRAY)

cv2.imshow("아이유", img)
cv2.imshow("아이유-회색", gray)

cv2.waitKey(0)
cv2.destoryAllWindows()