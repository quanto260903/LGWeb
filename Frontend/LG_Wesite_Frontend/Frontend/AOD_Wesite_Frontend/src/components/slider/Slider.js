'use client';

import React from 'react';

// // Import Swiper React components
import { Swiper, SwiperSlide } from 'swiper/react';

// import required modules
import { Pagination } from 'swiper/modules';

import 'swiper/css';
import 'swiper/css/pagination';
import Link from 'next/link';
import Image from 'next/image';

import SliderItem from '../slider/SliderItem'

const Slider = () => {

  const pagination = {
    clickable: true,
    renderBullet: function (index, className) {
      return '<span class="' + className + '">' + (index + 1) + '</span>';
    },
  };

  return (
    <div className='slider'>
      <Swiper pagination={pagination}>
        <SwiperSlide>
          <SliderItem />
        </SwiperSlide>
        <SwiperSlide>
          <SliderItem />
        </SwiperSlide>
        <SwiperSlide>
          <SliderItem />
        </SwiperSlide>
      </Swiper>
    </div>

  );
};

export default Slider;