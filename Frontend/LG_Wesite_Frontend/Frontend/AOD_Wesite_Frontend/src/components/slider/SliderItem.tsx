'use client';

import React, { useState, useEffect } from 'react';

import Link from 'next/link';
import Image from 'next/image';

// Import Swiper components
import { Swiper, SwiperSlide } from 'swiper/react';
import { Pagination } from 'swiper/modules';

import 'swiper/css';
import 'swiper/css/pagination';

import sliderImage from './../../app/images/slider.png';

import { BlogCategoryType } from '@/type/BlogCategory';
import { getSlideHomeData } from '@/api/slidehometop';

const SliderItem: React.FC = () => {
  const [data, setData] = useState<BlogCategoryType[]>([]);

  useEffect(() => {
    const loadData = async () => {
      const result = await getSlideHomeData();
      setData(result);
    };
    loadData();
  }, []);


  return (
    <div className="wrp">
      <Swiper
        pagination={{ clickable: true }} // Thêm tính năng pagination với thuộc tính clickable
        modules={[Pagination]}
        slidesPerView={1}
        spaceBetween={30}
        loop={true} // Cho phép lặp lại slide
      >
        {data && data.map((item, index) => (
          <SwiperSlide>
            <div key={index} className="slider-item">
              <div className="content">
                <div className="slider-title">{item.name}</div>
                <p className="description">
                  {item.description}
                </p>
                <Link href="/" className="read-more-btn">Read more</Link>
              </div>
              <div className="image">
                <img
                    src={item.imageUrl || '/picture/NoImage.png'}
                    style={{ width:'500' , height:'auto' }}
                    alt={item.name || 'Image'}
                  />
              </div>
            </div>
          </SwiperSlide>
        ))}
      </Swiper>
    </div>
  );
};

export default SliderItem;
