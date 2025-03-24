'use client';

import React, { useState, useRef, useEffect } from 'react';
import Link from 'next/link';
import Image from 'next/image';
// Import Swiper components
import { Swiper, SwiperSlide } from 'swiper/react';
// Import required modules
import { Pagination } from 'swiper/modules';

import 'swiper/css';
import 'swiper/css/pagination';
import '../app/css/abstracts/_variables.scss';
import p1 from './../app/images/ourTeam.png';
import '../app/css/pages/_about.scss';
import '../app/css/team.css';
import Check from './../app/images/Check.png';

import { BlogCategoryType } from '@/type/BlogCategory';
import { GetAboutTeam } from '@/api/about';

const Team: React.FC = () => {
  const [activeTab, setActiveTab] = useState<number>(0); // State to track active tab
  const swiperRef = useRef<any>(null); // Ref for Swiper instance

  const pagination = { clickable: true };

  // Update tab and slide on tab click
  const handleTabClick = (index: number) => {
    setActiveTab(index);
    if (swiperRef.current) {
      swiperRef.current.slideTo(index);
    }
  };

  // Sync tab state with slide change
  const handleSlideChange = (swiper: any) => {
    setActiveTab(swiper.activeIndex);
  };

  const [data, setData] = useState<BlogCategoryType[]>([]);

  useEffect(() => {
    const loadData = async () => {
      const result = await GetAboutTeam();
      setData(result);
    };
    loadData();
  }, []);

  return (
    <div className="team-slider">
      <div className="team-content">
        <h3 className="small-title tl">\ Team \</h3>
        <h2 className="team-title tl2">Our team</h2>

        {/* Tabs */}
        <div className="tab">
          <span
            className={`tabItem ${activeTab === 0 ? 'active' : ''}`}
            onClick={() => handleTabClick(0)}
          >
            Resource scheduling
          </span>
          <span
            className={`tabItem ${activeTab === 1 ? 'active' : ''}`}
            onClick={() => handleTabClick(1)}
          >
            Digital signage
          </span>
          <span
            className={`tabItem ${activeTab === 2 ? 'active' : ''}`}
            onClick={() => handleTabClick(2)}
          >
            Add-on for Exchange and Outlook
          </span>
        </div>

        <Swiper
          pagination={pagination}
          slidesPerView={1}
          spaceBetween={30}
          modules={[Pagination]}
          loop={true}
          onSwiper={(swiper) => (swiperRef.current = swiper)}
          onSlideChange={handleSlideChange}
        >
          {data && data.map((item, index)=> (
              <SwiperSlide key={index}>
                <div className="team-info wrp">
                  <div className="team-text">
                    <h3 className="tl3">{item.name}</h3>
                    <p className="tl4">
                      {item.description}
                    </p>
                    <ul className="tl4" dangerouslySetInnerHTML={{ __html: item.detail }}>
                    </ul>
                  </div>
                  <div className="team-image">
                    <img
                      src={item.imageUrl || '/picture/NoImage.png'}
                      style={{ width: '100%', height: 'auto' }}
                      alt={item.name || 'Image'}
                      
                    />
                  </div>
                </div>
              </SwiperSlide>
            ))}
        </Swiper>
      </div>
    </div>
  );
};

export default Team;
