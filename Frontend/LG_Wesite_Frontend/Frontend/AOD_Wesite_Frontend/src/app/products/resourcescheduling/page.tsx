"use client"
import Image from "next/image";
import React, { useState, useEffect } from 'react';
import shedulingImage from './../../../app/images/scheduling.png';
import productImage from './../../../app/images/products.png';
import shapeImage from './../../../app/images/Shape.png';
import iconYoutube from './../../../app/images/IconYoutube.png';
import '../../css/pages/_product.scss';
import './../../css/abstracts/_variables.scss';
import { BlogCategoryType,ProductType } from '@/type/BlogCategory';
import { GetResourcescheduling } from '@/api/product';
export default function ResourceScheduling() {
  const [data, setData] = useState<{ listBlogCategory: BlogCategoryType[] } | null>(null);
  useEffect(() => {
    const loadData = async () => {
      const result = await GetResourcescheduling();
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
        <h3 className="tl">\ Resource scheduling \</h3>
        <h1 className="tl2">Resource scheduling</h1>
      </div>
      {data && data.listBlogCategory && data.listBlogCategory.map((item, index) => (
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
      ))}
    </div>
  );
}
