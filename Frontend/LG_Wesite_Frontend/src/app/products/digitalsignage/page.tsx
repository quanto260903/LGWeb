"use client"
import Image from "next/image";
import React, { useState, useEffect } from 'react';
import shedulingImage from './../../../app/images/scheduling.png';
import productImage from './../../../app/images/products.png';
import shapeImage from './../../../app/images/Shape.png';
import iconYoutube from './../../../app/images/IconYoutube.png';
import { BlogCategoryType } from '@/type/BlogCategory';
import { GetDigitalSignage } from '@/api/product';
export default function DigitalSignage() {
  const [data, setData] = useState<{ listBlogCategory: BlogCategoryType[] } | null>(null);
  useEffect(() => {
    const loadData = async () => {
      const result = await GetDigitalSignage();
      setData(result);
    };
    loadData();
  }, []);
  return (
    <div className="resource-scheduling">
      <div className="product-header">
        <div className="image-overlay">
          <Image
            src={productImage}
            alt="Software development"
            className="background-image"
          />
        </div>
        <div className="overlay-text">Products</div>
      </div>
      <div className="header-section">
        <h3 className="tl">\ Digital signage \</h3>
        <h1 className="tl2">Digital signage</h1>
      </div>
      <div className="product-page">
      {data && data.listBlogCategory && data.listBlogCategory.map((item, index) => (
         item.order % 2 !== 0 ? (
        
        <div className="describe wrp">
          <div className="resource-section">

            <div className="text-column">
              <h1 className="tl44">{item.name} </h1>
              <p className="body1">
                {item.description}
              </p>
              <button className="contact-button">Contact now</button>
            </div>
            <div className="image-column">
              <Image
                src={shedulingImage}
                alt="TFS 2018"
                width={400} // Adjust the image size
                height={300} // Adjust the image size
              />
            </div>
          </div>
          <div className="features-and-videos wrp">
            <div className="features-section">
              <div className="title">
                <Image
                  src={shapeImage}
                  alt="Software development"
                />
                <h1 className="tl44">Features</h1>
              </div>
              <ul>
                {item.features && item.features.split('|').map((text) => {
                  return <>
                    <li>{text}</li>
                  </>
                })}
              </ul>
            </div>
            <div className="video-demo">
              <div className="title">
                <Image
                  src={iconYoutube}
                  alt="Software development"
                />
                <h3 className="tl44">Video Demo</h3>
              </div>
              <div className="video-thumbnails">
              {item.videos && item.videos.map((video, index) => {
                  return <>
                    <div className="video-thumbnail" key={index}>
                      <iframe
                        src={video.linkUrl}
                        frameBorder="0"
                        allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                        allowFullScreen
                      ></iframe>
                      <p className="body2">{video.description}</p>
                    </div>
                  </>
                })}
              </div>
            </div>
          </div>
        </div>
      
      ) : (
 <div className="describe wrp">
      <div className="resource-section wrp reverse-order">
        <div className="text-column">
          <h1 className="tl44">{item.name}</h1>
          <p className="body1">
           {item.description}
          </p>
          
          <button className="contact-button">Contact now</button>
        </div>

        <div className="image-column">
          <Image
            src={shedulingImage}
            alt="TFS 2018"
            width={400} // Adjust the image size
            height={300} // Adjust the image size
          />
        </div>
      </div>
      <div className="features-and-videos wrp">
        <div className="features-section">
          <div className="title">
            <Image
              src={shapeImage}
              alt="Software development"
            />
            <h1 className="tl44">Features</h1>
          </div>
          <ul>
                {item.features && item.features.split('|').map((text) => {
                  return <>
                    <li>{text}</li>
                  </>
                })}
              </ul>
        </div>

        <div className="video-demo">
          <div className="title">
            <Image
              src={iconYoutube}
              alt="Software development"
            />
            <h3 className="tl44">Video Demo</h3>
          </div>
          <div className="video-thumbnails">
              {item.videos && item.videos.map((video, index) => {
                  return <>
                    <div className="video-thumbnail" key={index}>
                      <iframe
                            src={video.linkUrl}
                        frameBorder="0"
                        allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                        allowFullScreen
                      ></iframe>
                      <p className="body2">{video.description}</p>
                    </div>
                  </>
                })}
              </div>
        </div>
      </div>
    </div>
    
    )
  ))}
  </div>
    </div>
  );
}
