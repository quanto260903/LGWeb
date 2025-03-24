'use client';

import React, { useState, useRef, useEffect } from 'react';
import Link from 'next/link';
import Image from 'next/image';
import { Swiper, SwiperSlide } from 'swiper/react';
import { Pagination } from 'swiper/modules';
import 'swiper/css';
import 'swiper/css/pagination';
import '../app/css/product.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheckCircle } from '@fortawesome/free-solid-svg-icons';
import p1 from './../app/images/ourTeam.png';
import Check from './../app/images/Check.png';
import '../app/css/pages/_product.scss';

import { BlogCategoryType } from '@/type/BlogCategory';
import { GetDigitalSignage, GetExchangeandOutlook, GetResourcescheduling, GetProduct } from '@/api/product';
import { CategoryType} from '@/type/Category';
export default function Product() {

 
  const [activeTab, setActiveTab] = useState(0); // State to track active tab
  const [data, setData] = useState<{ listBlogCategory: BlogCategoryType[] } | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const swiperRef = useRef(null);

  const [datatab, setDataTab] = useState<CategoryType[]>([]);
  const pagination = { clickable: true };

  useEffect(() => {
    const loadData = async () => {
        const result = await GetProduct();
        setDataTab(result);
debugger;
    };
    loadData();
}, []);

  // Sync tab state with slide change
  const handleSlideChange = (swiper) => {
    setActiveTab(swiper.activeIndex);
  };

  // Update tab and slide on tab click
  const handleTabClick = async (index) => {
    setActiveTab(index);
    setLoading(true);
    setError(null);

    try {
      let result;
      if (index === 0) {
        result = await GetResourcescheduling();
      } else if (index === 1) {
        result = await GetDigitalSignage();
      } else if (index === 2) {
        result = await GetExchangeandOutlook();
      }
      setData(result);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  // Call API when the component mounts to load the first tab's data
  useEffect(() => {
    handleTabClick(0);
  }, []);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <div className="product-slider">
      <div className="team-content">
        <p className="tl">\ Product \</p>
        <p className="tl2">Our products</p>

        {/* Tabs */}
        <div className="tabs">
        {datatab && datatab.map((item, index)=> (
          <span
            className={`tabItem ${activeTab === 0 ? 'active' : ''}`}
            onClick={() => handleTabClick(0)}
          >
            {item.name}
          </span>
          
          ))}
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
         {data && data.listBlogCategory && data.listBlogCategory.map((item, index) => (
            <SwiperSlide key={index}>
              <div className="team-info wrp">
                <div className="team-text">
                  <h3 className="tl3">{item.name}</h3>
                  <p className="tl4">
                   {item.description}
                  </p>
                  <ul className="tl4">
                    <li>
                      <Link href="/">
                        <Image
                          src={Check}
                          alt="Contact icon"
                          className="icon-salary"
                        />
                      </Link>
                      Premium resource management solution
                    </li>
                    <li>
                      <Link href="/">
                        <Image
                          src={Check}
                          alt="Contact icon"
                          className="icon-salary"
                        />
                      </Link>
                      Meeting arrangements in less than 2 minutes
                    </li>
                    <li>
                      <Link href="/">
                        <Image
                          src={Check}
                          alt="Contact icon"
                          className="icon-salary"
                        />
                      </Link>
                      Real-time order overview
                    </li>
                  </ul>
                       <div className="button-container">
          <Link href={'/about'}>
            <button className="read-more-btn">Read more</button>
          </Link>
        </div>
                </div>
                <div className="team-image">
                <iframe
        src="https://www.youtube.com/embed/h8J4nEiQyOo"
        frameBorder="0"
        allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
        allowFullScreen
      ></iframe>
                </div>
              </div>
            </SwiperSlide>
          ))}
        </Swiper>
      </div>
    </div>
  );
}
